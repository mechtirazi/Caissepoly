   M S B A M L   `   `   `  ����  
CaissePoly&     CaissePoly.Componnents.SearchBox   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � .+�5	   
   ��   �� Orientation$  
Horizontal=�6   .+�5
      �� -   5         ��Name$ SearchTextBox��5
      $	��300��5      $��10q�5      $��14(�5      
���������5      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�.��5   (   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 �   �67    �� Triggers 5      �� 5       �� Binding 6*   �� *Text+ �� RelativeSource �� *Self+	 �� Value	  6i   .a�5   "   ��  � ��
 �����6?   �� 5   "    � ��$ Rechercher un article...��69   5      P� 5      
 P� Property
 �@J�IsKeyboardFocused P� Value$
 True��6C   . �5   "   ��  � ��
 �   �6?   5      5      .��5   !   5      �� 5       $
��🔍��6   $��10,0,0,0q�5!      $��10q�5"      ��5#      �� *SearchCommand+5$      5%      5&      