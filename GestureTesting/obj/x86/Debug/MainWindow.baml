   M S B A M L   `   `   `  ����P  LCoding4Fun.Kinect.Wpf, Version=1.5.0.0, Culture=neutral, PublicKeyToken=nullnKclr-namespace:Coding4Fun.Kinect.Wpf.Controls;assembly=Coding4Fun.Kinect.WpfCoding4Fun.Kinect.Wpf.Controls  5   	   S OMicrosoft.Kinect.Toolkit, Version=1.6.0.0, Culture=neutral, PublicKeyToken=nulleHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.ToolkitMicrosoft.Kinect.Toolkit 5   j   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 6�    GestureTesting   GestureTesting.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   `ControlsKclr-namespace:Coding4Fun.Kinect.Wpf.Controls;assembly=Coding4Fun.Kinect.Wpf     5   	   WmyHclr-namespace:Microsoft.Kinect.Toolkit;assembly=Microsoft.Kinect.Toolkit     5   j   6my1&clr-namespace:System;assembly=mscorlib     6�   -   5          � Title$  
MainWindow��5   	   $	��813��6   $	��789��6)   $��Center=�65    � WindowState$ 	Maximized=�6R    � 	Resources 5         
ImageSize1��%#   &	        X�5   
   0Q�6+   5      .��5	      � -   5        $
��grid��5	      
���   �.+�5
   
   4  .Microsoft.Kinect.Toolkit.KinectSensorChooserUI  -   5        $��SensorChooserUi��5
   Q   $
��Left=�6#   $��	0,271,0,0q�6>   $��Bottom=�6h   0   *Coding4Fun.Kinect.Wpf.Controls.HoverButton  5   
   -   5        $��HButton��5      $��HB��6.   
�������$��46,101,666,625q�6N   �� 5   
   -   5        $��
lblManoDer��5   I   $��ManoDer��6   $��35��6"   $
��Left=�6.   $��Bottom=�6[   $��Center=�6w   $��20(�6�   $��467,0,0,165q�6�   $	��300��6�   $��Normal"�6�   $��Normal$�6�   $��Right=�6�   ��5      �� 5       �� EndPoint$ 1,0.5&�6&    �� 
StartPoint$ 0,0.5&�67   .��5      �  � Color$ Black��6#    � Offset$ 0Q�61   � 5      $ White��6#   $ 0Q�61   5      5      �� 5   
   -   5        $��SkeletonFeedbackImage��5      $��Uniform=�6-   $��0,0,0,0q�6?   $��Right=�6P   $��80��6l   $��60��6w   $	��Top=�6�   �� 5   
   -   5        $��lblMode��5   [   $
��Mode��6   $��35��6   $
��Left=�6+   $��467,0,0,206q�6F   $��Bottom=�6j   $��20(�6�   $	��300��6�   $��Normal"�6�   $��Normal$�6�   $��Right=�6�   ��5      �� 5      $ 1,0.5&�6&   $ 0,0.5&�67   .��5      � $ Black��6#   $ 0Q�61   � 5      $ White��6#   $ 0Q�61   5      5      �� 5   
   -   5        $��
lblManoIzq��5   l   $��ManoIzq��6   $��20(�6"   $��35��60   $
��Left=�6<   $��467,0,0,124q�6W   $��Bottom=�6~   $	��300��6�   $��Normal"�6�   $��Normal$�6�   $��Right=�6�   ��5      �� 5      $ 1,0.5&�6&   $ 0,0.5&�67   .��5       � $ Black��6#   $ 0Q�61   � 5!      $ White��6#   $ 0Q�61   5"      5$      �� 5%   
   -	   5        $��lblDistance��5%   s   $��Distancia Joint��6   $��20(�6*   $��35��68   $
��Left=�6D   $��
467,0,0,83q�6_   $��Bottom=�6�   $	��300��6�   $��Normal"�6�   $��Normal$�6�   $��Right=�6�   ��5&      �� 5'      $ 1,0.5&�6&   $ 0,0.5&�67   .��5(      � $ Black��6#   $ 0Q�61   � 5)      $ White��6#   $ 0Q�61   5*      5,      �� 5-   
   -
   5        $��image1��5-   M   $	��354��6   $
��Left=�6   $��255,118,0,0q�68   $
��Fill=�6[   $	��Top=�6j   $	��448��6�   �� 5.   
   -   5        $��lblInitialMove��5.   J   $��35��6   $��Right=�6   $��0,0,0,42q�68   $��Bottom=�6`   $	��300��6{   $��20(�6�   $��Initial��6�   $��Normal"�6�   $��Normal$�6�   $��Right=�6�   ��5/      �� 50      $ 1,0.5&�6&   $ 0,0.5&�67   .��51      � $ Black��6#   $ 0Q�61   � 52      $ White��6#   $ 0Q�61   53      55      �� 56   
   -   5        $��lblCurrentMove��56   8   $��35��6   $��Right=�6   $��Bottom=�6N   $	��300��6i   $��20(�6u   $��Current��6�   $��Normal"�6�   $��Normal$�6�   $��Right=�6�   ��57      �� 58      $ 1,0.5&�6&   $ 0,0.5&�67   .��59      � $ Black��6#   $ 0Q�61   � 5:      $ White��6#   $ 0Q�61   5;      5=      �� 5>   
   -   5        $��	btnZoomIn��5>   `   $��Button��6   $��23��6"   $��Center=�6.   $��380,749,0,0q�6K   $��Bottom=�6q   $��75��6�   6�   �� 5?   
   -   5        $��
btnZoomOut��5?   `   $��Button��6   $��23��6"   $��Center=�6.   $��255,751,0,0q�6K   $��Bottom=�6r   $��75��6�   6�   �� 5@   
   -   5        $��canvas1��5@   M   $	��290��6   $
��Left=�6   $��
12,197,0,0q�69   $	��Top=�6\   $	��237��6t   5A      5B      