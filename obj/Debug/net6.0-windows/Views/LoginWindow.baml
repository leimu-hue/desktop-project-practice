   M S B A M L   `   `   `  ����M  IIntelligentControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:IntelligentControl.BaseIntelligentControl.Base  5   	   C&clr-namespace:IntelligentControl.ViewsIntelligentControl.Views  5   	   *    $IntelligentControl.Views.LoginWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6base%clr-namespace:IntelligentControl.Base     5   	   8local&clr-namespace:IntelligentControl.Views     5   	      
windowIcon  � Icon#  [  5	   	     defaultLoginTitle � Title# [ 5
   	    � 
ResizeMode$ CanMinimize=�64    � WindowStartupLocation$ CenterScreen=�5   	   $	��800��5   	   $	��400��6    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5   
   '  !System.Windows.Shell.WindowChrome   WindowChrome 5        5   
      CaptionHeight$ 15Q�6      GlassFrameThickness$ 0q�5         CornerRadius$ 0��5      	 � 	Resources	 5      
 �� Source�� 5   
   $b
 \pack://application:,,,/IntelligentControl;component/resourceStyle/loginWindowDictionary.xaml?�6   %    .��5      � -   5           IsHitTestVisibleInChrome$
 True��5       � RowDefinitions 5   
   �� 5      $��1* �6   6*    � ColumnDefinitions 5   
   �� 5      $
��1.6* �6   6.   �� 5       6!   .+�5#   
   �  � Points+ �    ��t� �� � �� ��$
��Left=�5$      $��0��5%      ��5&      �� 5'        loginImageBrush �� ImageSource# [ 6    j� Stretch$ UniformToFill=�6L    5)        5*      5,      � 5-   
   $��1��6   $��	0 0 10 10q�6    5.      �� 5/      $��30 �6    �� 50      �� 51      $��20 �6    �� 52      �� 53      �� 54      �� 55      �� 56      $��40��6    $	��100��6/   .+�59      �� $��Right=�6   $��40��61   $��30��6<     CloseButtonTemplate#��[ 6H   ��5:      �� *CloseCommand+��5;      �� *.+ �� RelativeSource ��  �� AncestorType
 Window6k   �� 5>      $��1��6   .+�5?      �� $��LM智控��6     
loginTitle#��[ 6(   6N   �� 5@      $��用来练习的项目��6     littleLoginTitle#��[ 6+   6W   5A      �� 5B      -   5        $��userNameText��5B        userNameTextBlockStyle#��[ 6�   $��3��6�   ��6*   �� *UserModel.UserName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   G� 5C      -   5        $��passwordText��5C      $��4��6.   /  )IntelligentControl.Base.PasswordBoxHelper   Attach$
 true��5E        passwordTextBlockStyle#��[ 5F         PasswordContent 5D      �� *UserModel.Password+$ TwoWay=�$ PropertyChanged=�5F   L   ]� 5G      $��False��6   $��5��6.   .+�5H      �� -   5        $��remeberLoginMessage��5H      $��记住登录信息��66   $
��Left=�6G   $��40��5I      $��Center=�6'   $��12(�6B   6R   �� 5J      $��Right=�6   $��60��63   $��40��6>   $��Center=�5K      $��12(�67   �� 5L      .�6    忘记密码6&   5M      5N      ]� 5O      $
��True��6   $��6��6-   .+�5P      �� $��
登    录��6   ���	#FF104991
���������5R      �� *LoginCommand+��5S      �� *.+ �� 
 Window6o   5T      �� 5U      $
��True��6   $��Hidden=�63   $��Hidden=�5V      $	��100��6B   $��7��5W      .��5X      �� 
���  �� �� TextAlignment$ Center=�6K   $	��278��6b   ��6   �� *ErrorMessage+6p   5Y      5Z      5[      5\      