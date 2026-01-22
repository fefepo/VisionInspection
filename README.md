# VisionInspection (C# WinForms + OpenCV)

C# WinForms와 OpenCV(OpenCvSharp)를 활용하여  
**이미지 기반 결함 검출(머신비전 검사) 시스템**을 구현한 개인 프로젝트입니다.

Threshold 기반 이진화, OTSU 자동 임계값, ROI 선택,  
Contour 기반 결함 시각화 및 정량 분석 기능을 포함합니다.

---

## 🔧 사용 기술
- C#
- WinForms (Windows Forms)
- OpenCV (OpenCvSharp)
- Image Processing
- Computer Vision

---

## 📌 주요 기능

### 1. 이미지 로드
- JPG / PNG / BMP 이미지 불러오기
- 원본 이미지 표시

### 2. 이진화 기반 검사
- **수동 Threshold 조절**
- **OTSU 자동 Threshold 적용**
- 그레이스케일 → 이진화 처리

### 3. ROI(관심영역) 선택 검사
- 마우스 드래그로 ROI 영역 지정
- ROI 영역만 대상으로 결함 검사 수행

### 4. Contour 기반 결함 검출
- 이진 이미지에서 Contour 검출
- 결함 영역을 **Bounding Box로 시각화**
- 결함 개수(Defects Count) 계산
- 결함 면적(Area) 정량 출력

### 5. 판정 로직 (PASS / FAIL)
- 흰 픽셀 비율(Fail Ratio) 기준 판정
- 기준 초과 시 FAIL, 미만 시 PASS

---

## 🧠 검사 로직 개요

1. 입력 이미지 → Grayscale 변환
2. Threshold 또는 OTSU 기반 이진화
3. ROI 적용 (선택 시)
4. Contour 검출
5. 결함 개수 및 면적 계산
6. Fail Ratio 기준으로 PASS / FAIL 판정

---

## 🖼 실행 화면
- 좌측: 원본 이미지 + ROI 표시
- 우측: 이진화 결과 + 결함 영역 시각화
- 상단: Threshold 값, Fail Ratio, 결함 개수/면적 표시

---

## 🎯 프로젝트 목적
- 머신비전 검사 로직의 기본 구조 이해
- 영상 처리 기반 품질 검사 알고리즘 구현
- C# + OpenCV 실무 적용 경험 축적

---

## 📈 향후 개선 아이디어
- Adaptive Threshold / Morphology 적용
- 결함 크기별 분류
- CSV 결과 저장
- 카메라 실시간 입력 연동
