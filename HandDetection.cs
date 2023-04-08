namespace OpenCvSharp.Demo
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using OpenCvSharp;

	public class HandDetection: WebCamera
	{
		public int cX = 0, cY = 0;

		protected override void Awake()
		{
			base.Awake();
			this.forceFrontalCamera = true;
		}

		// Our sketch generation function
		protected override bool ProcessTexture(WebCamTexture input, ref Texture2D output)
		{
			Mat img = Unity.TextureToMat(input, TextureParameters);
			Mat src = img.Clone();
			//손 인식
			Mat YCrCb = new Mat ();
			//Mat mask1 = new Mat();
			//Mat mask2 = new Mat();
			Mat mask = new Mat();
			Mat handImage = new Mat();
			Mat gray = new Mat();

			//이미지 색상변경			
			Cv2.CvtColor (img, YCrCb, ColorConversionCodes.BGR2YCrCb);

			//손 색 범위 설정
			//Cv2.InRange(YCrCb, new Scalar(0, 133, 77), new Scalar(255, 173, 127), mask);
			Cv2.InRange(YCrCb, new Scalar(0, 134, 78), new Scalar(255, 172, 126), mask);

			//손만 색 있고 나머지는 검정
			Cv2.Add(img, new Scalar(0), handImage, mask);
			Cv2.Erode(handImage, handImage, new Mat(3, 3, MatType.CV_8U, new Scalar(1)), new Point(-1, -1), 2);

			//손바닥 중심 찾기
			//Mat dst = new Mat();
			//double radius;
			//Cv2.DistanceTransform(handImage, dst, DistanceTypes.L2, DistanceMaskSize.Mask5);
			//int[] maxidx = new int[2];
			//int[] minidx = new int[2];
			//Cv2.MinMaxIdx(dst, out double radius1, out radius, out minidx, out maxidx, handImage);
			//Point palm = new Point(maxidx[1], maxidx[0]);

			//Cv2.Circle(src, palm, 2, Scalar.Blue, -1);
			//Cv2.Circle(src, palm, (int) (radius+0.5), Scalar.Blue, -1);

			//손 윤곽선
			Cv2.CvtColor(handImage, gray, ColorConversionCodes.BGR2GRAY);

			Point[][] contours;
			HierarchyIndex[] hierarchy;
			Cv2.FindContours(gray, out contours, out hierarchy, RetrievalModes.Tree, ContourApproximationModes.ApproxTC89KCOS);

			List<Point[]> new_contours = new List<Point[]>();
			List<Point[]> hull = new List<Point[]>();
			int[] convexHullIdx;

			Moments moments;

			foreach(Point[] p in contours)
            {
				double length = Cv2.ArcLength(p, true);

				if (length > 100)
                {
					Point[] approx = Cv2.ApproxPolyDP(p, length * 0.01, true);
					hull.Add(Cv2.ConvexHull(p));
					new_contours.Add(p);
					convexHullIdx = Cv2.ConvexHullIndices(p);
					//손 끝점 찾기
					Vec4i[] defects = Cv2.ConvexityDefects(p, convexHullIdx);			
					foreach(Vec4i v in defects)
                    {
						double dist = v.Item3 / 256.0;
						if(dist > 1) Cv2.Circle(src, p[v.Item0], 3, Scalar.Red, -1, LineTypes.AntiAlias);
					}
					//손 중심 찾기
					foreach(Point c in approx)
                    {
						if(c.Length() > 4)
                        {
							moments = Cv2.Moments(approx, true);

							cX = Convert.ToInt32(moments.M10 / moments.M00);
							cY = Convert.ToInt32(moments.M01 / moments.M00);

							Cv2.Circle(src, new Point(cX, cY), 5, Scalar.Black, -1);
						}
                    }
				}
            }
						
			Cv2.DrawContours(src, new_contours, -1, new Scalar(255, 0, 0), 2, LineTypes.AntiAlias, null, 1);
			Cv2.DrawContours(src, hull, 0, new Scalar(0, 255, 0), 2, LineTypes.AntiAlias);

			//Debug.Log("x좌표 : "+cX+" y좌표 : "+cY);

			//Cv2.CvtColor(handImage, handImage, ColorConversionCodes.BGR2YCrCb);

			output = Unity.MatToTexture(src, output);
			return true;
		}
	}
}