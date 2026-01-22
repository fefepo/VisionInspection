using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;


namespace VisionInspection
{
    public partial class Form1 : Form
    {
        private Mat srcImage;
        private int _lastUsedThreshold = 120; // 실제 사용된 threshold 기록용

        // ROI 드래그 상태
        private bool _isDragging = false;
        private System.Drawing.Point _dragStart;
        private Rectangle _roiRectPB = Rectangle.Empty;   // PictureBox 좌표계 ROI
        private Rect _roiRectImg = new Rect();            // OpenCV(Mat) 좌표계 ROI


        private Rectangle MakeRect(System.Drawing.Point p1, System.Drawing.Point p2)
        {
            int x = Math.Min(p1.X, p2.X);
            int y = Math.Min(p1.Y, p2.Y);
            int w = Math.Abs(p1.X - p2.X);
            int h = Math.Abs(p1.Y - p2.Y);
            return new Rectangle(x, y, w, h);
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void labelResult_Click(object sender, EventArgs e)
        {

        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    // OpenCV로 이미지 읽기
                    srcImage = Cv2.ImRead(ofd.FileName);

                    if (srcImage.Empty())
                    {
                        MessageBox.Show("이미지를 불러올 수 없습니다.");
                        return;
                    }

                    // PictureBox에 표시
                    pictureBoxSrc.Image = BitmapConverter.ToBitmap(srcImage);

                    labelResult.Text = "RESULT: LOADED";
                }
            }


        }

        private void buttonInspect_Click(object sender, EventArgs e)
        {

            if (srcImage == null || srcImage.Empty())
            {
                MessageBox.Show("먼저 이미지를 불러오세요.");
                return;
            }

            // 1) 여기부터 추가 (ROI 적용 여부 판단)
            Mat inspectSrc = srcImage;

            if (_roiRectPB != Rectangle.Empty)
            {
                // ROI가 선택되어 있으면 그 영역만 검사
                inspectSrc = new Mat(srcImage, _roiRectImg);
            }


            // 1) 그레이 변환, 여기서부터는 "inspectSrc" 기준으로 처리
            Mat gray = new Mat();
            Cv2.CvtColor(inspectSrc, gray, ColorConversionCodes.BGR2GRAY);


            // 2) 이진화 (OTSU or 수동)
            Mat binary = new Mat();

            if (checkAutoThreshold.Checked) // ★ OTSU 모드
            {
                double otsuTh = Cv2.Threshold(
                    gray,
                    binary,
                    0,
                    255,
                    ThresholdTypes.Binary | ThresholdTypes.Otsu
                );

                _lastUsedThreshold = (int)Math.Round(otsuTh);
            }
            else // ★ 수동 Threshold
            {
                int th = trackThreshold.Value;
                Cv2.Threshold(gray, binary, th, 255, ThresholdTypes.Binary);
                _lastUsedThreshold = th;
            }

            // 실제 사용된 threshold 화면 표시
            labelThreshold.Text = $"Threshold: {_lastUsedThreshold}";




            // 3) 노이즈 제거 (morphology)

            Mat cleaned = new Mat();
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(3, 3));
            Cv2.MorphologyEx(binary, cleaned, MorphTypes.Open, kernel);   // 작은 점 제거
            Cv2.MorphologyEx(cleaned, cleaned, MorphTypes.Close, kernel); // 구멍 메움


            // 4) Contour 검출

            OpenCvSharp.Point[][] contours;
            HierarchyIndex[] hierarchy;

            Cv2.FindContours(
                cleaned,
                out contours,
                out hierarchy,
                RetrievalModes.External,
                ContourApproximationModes.ApproxSimple
            );


            // 5) 결함 필터링 + 통계

            double minArea = 300; // 너무 작은 건 노이즈로 제거 (필요시 300~2000 조절)

            var defectContours = contours
                .Where(c => Cv2.ContourArea(c) >= minArea)
                .ToArray();

            int defectCount = defectContours.Length;
            double defectArea = defectContours.Sum(c => Cv2.ContourArea(c));

            // 라벨- 개수/면적 출력

            labelDefectCount.Text = $"Defects: {defectCount}";
            labelDefectArea.Text = $"Area: {(int)defectArea}";


            // 6) 시각화: 빨간 박스

            Mat vis = new Mat();
            Cv2.CvtColor(cleaned, vis, ColorConversionCodes.GRAY2BGR);

            foreach (var c in defectContours)
            {
                var rect = Cv2.BoundingRect(c);
                Cv2.Rectangle(vis, rect, new Scalar(0, 0, 255), 2);
            }

            // 결과 표시 (binary 대신 vis 표시!)
            pictureBoxResult.Image?.Dispose();
            pictureBoxResult.Image = BitmapConverter.ToBitmap(vis);


            // 7) PASS / FAIL (면적 기반)
            // Fail Ratio(%)를 "허용 결함 면적 비율"로 해석
            double failAreaThreshold = (trackRatio.Value / 100.0) * (cleaned.Rows * cleaned.Cols);
            labelResult.Text = (defectArea >= failAreaThreshold) ? "RESULT: FAIL" : "RESULT: PASS";

        }


        private Rect PictureBoxRectToImageRect(PictureBox pb, Mat img, Rectangle roiPB)
        {
            // PictureBox에 실제로 그려진 이미지 영역(레터박스 포함)을 계산
            float imageAspect = img.Width / (float)img.Height;
            float boxAspect = pb.ClientSize.Width / (float)pb.ClientSize.Height;

            int drawWidth, drawHeight, offsetX, offsetY;

            if (imageAspect > boxAspect)
            {
                // 가로에 맞춰짐 (위아래 여백)
                drawWidth = pb.ClientSize.Width;
                drawHeight = (int)(drawWidth / imageAspect);
                offsetX = 0;
                offsetY = (pb.ClientSize.Height - drawHeight) / 2;
            }
            else
            {
                // 세로에 맞춰짐 (좌우 여백)
                drawHeight = pb.ClientSize.Height;
                drawWidth = (int)(drawHeight * imageAspect);
                offsetY = 0;
                offsetX = (pb.ClientSize.Width - drawWidth) / 2;
            }

            // ROI를 실제 이미지 표시 영역 기준으로 클리핑
            Rectangle displayRect = new Rectangle(offsetX, offsetY, drawWidth, drawHeight);
            Rectangle clipped = Rectangle.Intersect(displayRect, roiPB);

            if (clipped.Width <= 0 || clipped.Height <= 0)
                return new Rect(0, 0, img.Width, img.Height); // ROI가 이미지 영역 밖이면 전체로

            // displayRect 기준 좌표로 변환
            int xInDisplay = clipped.X - offsetX;
            int yInDisplay = clipped.Y - offsetY;

            // displayRect -> 이미지 픽셀 좌표로 스케일링
            double scaleX = img.Width / (double)drawWidth;
            double scaleY = img.Height / (double)drawHeight;

            int x = (int)Math.Round(xInDisplay * scaleX);
            int y = (int)Math.Round(yInDisplay * scaleY);
            int w = (int)Math.Round(clipped.Width * scaleX);
            int h = (int)Math.Round(clipped.Height * scaleY);

            // 이미지 경계로 클램프
            x = Math.Max(0, Math.Min(x, img.Width - 1));
            y = Math.Max(0, Math.Min(y, img.Height - 1));
            w = Math.Max(1, Math.Min(w, img.Width - x));
            h = Math.Max(1, Math.Min(h, img.Height - y));

            return new Rect(x, y, w, h);
        }


        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackThreshold_Scroll(object sender, EventArgs e)
        {
            labelThreshold.Text = $"Threshold: {trackThreshold.Value}";
        }

        private void trackRatio_Scroll(object sender, EventArgs e)
        {
            labelRatio.Text = $"Fail Ratio(%): {trackRatio.Value}";
        }

        private void labelRatio_Click(object sender, EventArgs e)
        {

        }

        private void checkAutoThreshold_CheckedChanged(object sender, EventArgs e)
        {
            bool isAuto = checkAutoThreshold.Checked;

            // OTSU면 수동 threshold 조절 의미 없음
            trackThreshold.Enabled = !isAuto;

            // (선택) 체크 바꾸자마자 재검사
            if (srcImage != null && !srcImage.Empty())
            {
                buttonInspect_Click(null, null);
            }

        }

        private void pictureBoxResult_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBoxSrc_MouseDown(object sender, MouseEventArgs e)
        {
            if (srcImage == null || srcImage.Empty()) return;

            _isDragging = true;
            _dragStart = e.Location;
            _roiRectPB = new Rectangle(e.X, e.Y, 0, 0);

            pictureBoxSrc.Invalidate(); // 다시 그리기 요청
        }

        private void pictureBoxSrc_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;

            _roiRectPB = MakeRect(_dragStart, e.Location);
            pictureBoxSrc.Invalidate();
        }

        private void pictureBoxSrc_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_isDragging) return;
            _isDragging = false;

            _roiRectPB = MakeRect(_dragStart, e.Location);

            // 너무 작은 ROI는 무시
            if (_roiRectPB.Width < 10 || _roiRectPB.Height < 10)
            {
                _roiRectPB = Rectangle.Empty;
                pictureBoxSrc.Invalidate();
                return;
            }

            // PictureBox 좌표 -> 이미지(Mat) 좌표로 변환해서 _roiRectImg에 저장
            _roiRectImg = PictureBoxRectToImageRect(pictureBoxSrc, srcImage, _roiRectPB);

            pictureBoxSrc.Invalidate();
        }

        private void pictureBoxSrc_Paint(object sender, PaintEventArgs e)
        {
            if (_roiRectPB == Rectangle.Empty) return;

            using (var pen = new Pen(Color.Red, 2))
            {
                e.Graphics.DrawRectangle(pen, _roiRectPB);
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
