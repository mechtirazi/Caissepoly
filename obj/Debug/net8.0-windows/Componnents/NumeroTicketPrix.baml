   M S B A M L   `   `   `  ����  
CaissePoly-    'CaissePoly.Componnents.NumeroTicketPrix   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      � ���#F1F5F9  � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� 
������� �� CornerRadius$ 10��5       �� Padding$ 16q�5      ���#CBD5E1$��1q�5      $��0 0 0 16q�5      $��Center=�5      $	��350��5      ._�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�62   .+�5      �� $��N° Ticket :��6   $��SemiBold"�5      $��18(�5      ���#1E293B$��Center=�5      $��0,0,10,0q�5      �� 5      $	��200��6   $��38��5       $��16(�5!       �� 
IsEditable$
 True��5$      
����������#E2E8F0���#0F172A$��4q�5(      $��0q�5)      ��5"      �� *ListeTickets+ �� SelectedItem 5#      �� *SelectedTicket+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5)      5+      5,      � 5/   
   $��1��6   $��0q�6    � ColumnDefinitions 50      �� 51      $��1* �6#   �� 52      $��2* �6#   .+�56      �� $��0��6   
������ 
������ $��1q�59      $ 8��5:      $��0q�5;      �� 5>      
�������$��1q�5?      ���#CBD5E1$ 12��5A      $ 20q�5B      $��Center=�5C      $��2��60   $��100,0,100,0q�6D   ._�5D      �� $��Center=�6   .+�5E      �� $��Total à payer��6    $��16(�5F       ���#64748B$��Medium"�5H       $��Center=�5I       $��0,0,0,6q�5J       �� 5K      $��30(�5L       $
��Bold"�5M       ���#10B981$��Center=�5O       ��5K       �� *TotalTicket+	 �� StringFormat$	 	{0:F3} DT��5O       5P      5Q      5R      5S      5T      