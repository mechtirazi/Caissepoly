   M S B A M L   `   `   `  ����E  ACaissePoly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:CaissePoly.ViewsCaissePoly.Views  5   	   ?$clr-namespace:CaissePoly.ComponnentsCaissePoly.Componnents  5   	       CaissePoly.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0localclr-namespace:CaissePoly.Views     5   	   5comp$clr-namespace:CaissePoly.Componnents     5   	     � Title$  
CaissePoly��5   	   $	��700��5   	   $
��1302��5   	    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�5
   	    � AllowsTransparency$
 True��5   	   
������  � 
ResizeMode$ CanResizeWithGrip=�5   	   .��5      �� $��1q�6   ���#E0E0E0 �� CornerRadius$ 8��5      
�������._�5   
   � .+�5      ]� $��40��6   ���#3B82F6.+�5      �� $��
CaissePoly��6   
�������$��SemiBold"�5      $
��10,0q�5      $��Center=�5      �� 5        �� Orientation$ 
Horizontal=�6   $��Right=�5!      $��Right=�5"      
�������.+�5%      �� -   5        $��_��5&      5'      �� 5(      -   5        $	��□��5)      5*      �� 5+      -   5        $	��✕��5,      5-      5.      5/      � 52      $��0,32,0,0q�6    � RowDefinitions 53      �� 54      $��* �6$   �� 55      $
��Auto �6$   .+�59      �� $��0��6   ���#F0F9FF._�5<      !   CaissePoly.Views.CaisseView  !   CaissePoly.Views.CaisseView   Loaded$ CaisseView_Loaded��6'   5=      �� 5@      $��1��6   ���#E0F2FE���#1E3A8A.@�5C      �� .��5D      �� $��Prêt��6$   $��Italic$�60   5E      �� 5F      �� 5G      .��5H      �� ��6$   �� *DateTime.Now+	 �� StringFormat$	 {0:HH:mm:ss}��5I      5J      5K      5L      5M      
 � 	Resources
 5O         WindowButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� Triggers P� Property P� Value3  -System.Windows.Media.Effects.DropShadowEffect  DropShadowEffect   
BlurRadius   ShadowDepth   Opacity   Color��%   &	        &	 �    �� 5Q   
   " ��6*   .�5R      ��  � ��
 �����6+   �� 5S       � ��
 ���� 6+   �� 5T       � ��
 ���� 6,   �� 5U       � ��$ 0q�60   �� 5V       � ��$ 40��6&   �� 5W       � ��$ 32��6'   �� 5X       � ��$ 12(�6)   �� 5Y       � �� 5Z      �� 5[      " ��6&   .R�5\      �� #��z��6!   $ 0��5]   !   ._�5^      �� $��Center=�6/   $��Center=�5_   /   5`      5a      5c       5d      P� 5e       �@J�IsMouseOver$
 True��61   . �5f      ��  � �� �#2563EB63   5g      P� 5h       � ��$
 True��6/   . �5i      ��  � �� �#1D4ED863   5j      5l        5o   
   $ 10Q�5p      $ 2Q�5q      $	 0.2Q�5r      $ Black��5s      5t      5u      