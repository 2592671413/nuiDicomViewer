using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Coding4Fun.Kinect.Wpf;
using Dicom;
using Dicom.IO.Reader;
using Dicom.IO.Buffer;
using Dicom.Imaging.Render;
using Dicom.Imaging;
using Dicom.Network;
using System.IO;

namespace nuiDicomViewer
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        FileInfo[] regfiles;

        KinectSensorChooser sensorChooser = new KinectSensorChooser();
        KinectSensor sensor;
        //Zooming
        private int zoom = 0;
        private int currentMove;
        private int initialMove;
        private int imageWidth;
        private int imageHeight;
        private bool  initialOneLoop;

        //Atributos ColorStream 

        //Atributos SkeletonStream
        //Instacia de esqueleto, por defecto se setea en al primero en caso de
        //que hayan mas personajes detectado por el sensor
        private Skeleton[] skeletons = new Skeleton[0];
        private DrawingGroup dg;
        private DrawingImage di;
        //geometric controls

        public Main()
        {
            
            InitializeComponent();
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dg = new DrawingGroup();
            di = new DrawingImage(dg);
            
            string file = AppDomain.CurrentDomain.BaseDirectory + "//Images//dicom//CTStudy//1.2.840.113619.2.30.1.1762295590.1623.978668950.112.dcm";
            DirectoryInfo dir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "//Images//dicom//CTStudy//");
            var request = DicomCFindRequest.CreatePatientQuery(patientId: "");
            regfiles = dir.GetFiles("*.dcm");
            DicomDataset dataset = DicomFile.Open(file).Dataset;
            DicomImage image = new DicomImage(dataset);
            
            image1.Source = image.RenderImageSource(0);
            SkeletonFeedbackImage.Source = di;
            SensorChooserUi.KinectSensorChooser = sensorChooser;
            sensorChooser.KinectChanged += new EventHandler<KinectChangedEventArgs>(sensorChooser_KinectChanged);
            sensorChooser.Start();
        }

        void sensorChooser_KinectChanged(object sender, KinectChangedEventArgs e)
        {
            try
            {
                IniciaSensor(e.NewSensor);
                DetieneSensor(e.OldSensor);
            }
            catch 
            {
                sensorChooser.Stop();
            }
        }
        /// <summary>
        /// Inicia sensor
        /// </summary>
        private void IniciaSensor(KinectSensor sensor)
        {
            if (null == sensor)
            {
                
                return;
            }
            this.sensor = sensor;
            
            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
            sensor.SkeletonStream.Enable(parameters);
            sensor.SkeletonStream.Enable();
            
            sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            sensor.Start();
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            Skeleton first = GetFirstSkeleton(e);
            if (first == null)
            {
                return;
            }
            using (DrawingContext dc = dg.Open())
            {
                
                dc.DrawRectangle(Brushes.Black, null, new Rect(0.0, 0.0, 80.0f, 60.0f));
                if (first.TrackingState == SkeletonTrackingState.Tracked)
                {
                    Point ElbowR = SkeletonPointToMouseCursor(first.Joints[JointType.ElbowRight].Position);
                    Point HandR = SkeletonPointToMouseCursor(first.Joints[JointType.HandRight].Position);
                    Point ElbowL = SkeletonPointToMouseCursor(first.Joints[JointType.ElbowLeft].Position);
                    Point HandL = SkeletonPointToMouseCursor(first.Joints[JointType.HandLeft].Position);
                    lblManoDer.Content = "X: "+HandR.X+" , Y: "+HandR.Y;
                    lblManoIzq.Content = "X: " + HandL.X + " , Y: " + HandL.Y;
                    lblDistance.Content = GetJointDistance(first.Joints[JointType.HandRight].Position);

                    
                    if (HandR.Y < ElbowR.Y && HandL.Y < ElbowL.Y)
                    {
                        currentMove = Math.Abs(Convert.ToInt32(HandL.X - HandR.X));
                        lblCurrentMove.Content = currentMove;
                        if (initialOneLoop == false)
                        {
                            imageWidth = Convert.ToInt32(image1.ActualWidth);
                            imageHeight = Convert.ToInt32(image1.ActualHeight);
                            initialMove = currentMove;
                            lblInitialMove.Content = initialMove;
                            initialOneLoop = true;
                        }
                        zoom = currentMove - initialMove;
                        lblMode.Content = "Zoomer ON! = " + zoom;
                        image1.Width = imageWidth + zoom;
                        image1.Height = imageHeight + zoom;
                    }
                    else
                    {
                        
                        if (HandL.Y < ElbowL.Y)
                        {
                            lblMode.Content = "Slide left";
                            System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)HandL.X * 2, (int)HandL.Y * 2);
                            
                        }
                        else
                        {
                            if (HandR.Y < ElbowR.Y)
                            {
                                lblMode.Content = "Slide right";
                                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)HandR.X * 2, (int)HandR.Y * 2);
                            }
                        }
                    }

                    
                   


                    drawBone(first, dc);
                }
            }
            dg.ClipGeometry = new RectangleGeometry(new Rect(0.0, 0.0, 80.0f, 60.0f));
            using (DepthImageFrame dframe = e.OpenDepthImageFrame())
            {
                sensor.DepthStream.Range = DepthRange.Near;
            }
        }

        private void drawBone(Skeleton skeleton, DrawingContext dc)
        {
            Brush drawBrush = Brushes.Aqua;
            foreach (Joint joint in skeleton.Joints)
            {
                
                dc.DrawEllipse(drawBrush, null, this.SkeletonPointToScreen(joint.Position), 1, 1);
            }
        }

        private Point SkeletonPointToScreen(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution80x60Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
        private Point SkeletonPointToMouseCursor(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution640x480Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
        private int GetJointDistance(SkeletonPoint skelpoint)
        {
            // Convert point to depth space.  
            // We are not using depth directly, but we do want the points in our 640x480 output resolution.
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skelpoint, DepthImageFormat.Resolution80x60Fps30);
            return depthPoint.Depth;
        }

        private Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame sf = e.OpenSkeletonFrame())
            {
                if (sf == null)
                {
                    return null;
                }
                skeletons = new Skeleton[sf.SkeletonArrayLength];
                sf.CopySkeletonDataTo(skeletons);
                Skeleton first = (from s in skeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
                return first;
            }

        }
        private void DetieneSensor(KinectSensor sensor)
        {
            if (null == sensor)
            {
                return;
            }
            sensor.Stop();
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sensorChooser.Stop();
        }

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            HButton.Hovering();
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {

        }

        public static System.Windows.Media.ImageSource ConvertDrawingImage2MediaImageSource(System.Drawing.Image image)
        {
            using (var ms = new MemoryStream())
            {
                var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                bitmap.BeginInit();

                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                bitmap.StreamSource = ms;
                bitmap.EndInit();
                return bitmap;
            }
        }
    }
    
}
