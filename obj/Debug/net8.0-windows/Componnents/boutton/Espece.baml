   M S B A M L   `   `   `  ����E  ACaissePoly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:CaissePoly.Componnents.bouttonCaissePoly.Componnents.boutton  5      +    %CaissePoly.Componnents.boutton.Espece   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:CaissePoly.Componnents.boutton     5        � 	Resources  5	         PrimaryText �� Color  SecondaryText  SuccessColor  DangerColor  PrimaryColor  RoundButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius  MoneyButtonStyle �� BasedOn  DisplayStyle��%P  &	        &	 !     &	 B     &	 c     &	 �     &	 �     7   &	 �    7  7  &	     �� 5
   
   $ #111827��6.   �� 5   
   $ #6b7280��60   �� 5   
   $ #22c55e��6/   �� 5   
   $ #f43f5e��6.   �� 5   
   $ #2563eb��6/   �� 5   
   " ��6)   .�5      ��  � ��
 �����6+   �� 5       � ��8   6+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 16(�6)   �� 5       � ��$ 8q�6'   �� 5       � ��$ 15,10q�6(   �� 5       � ��$ 0q�60   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   $ 12��6K   ._�5      �� $��Center=�6/   $��Center=�5   +   5      5      5!      5"      �� 5$   
   " ��6)   8   .�5%      ��  � ��8  6+   �� 5&       � ��
 �����6+   �� 5'       � ��$ 18(�6)   5(      �� 5*   
   " ��6%   .�5+      ��  � ��$ 24(�6)   �� 5,       � ��$
 Bold"�6+   �� 5-       � ��$ Center=�64   �� 5.       � ��$ Center=�62   �� 5/       � ��$ 8q�6'   50      51      .��54      � ���#f3f4f6 � RowDefinitions 55   
   �� 56      $
��Auto �6   �� 57      $��* �6   �� 58      $
��Auto �6   .+�5<   
   �� $��0��6   #��[ 6   	 �� Padding$	 20,15q�6I   ._�5=      ]� .+�5>      �� $��💰 Paiement en Espèces��6   $��20(�5?      $
��Bold"�6&   
�������$
��Left=�5@   +   �� 5A      
������ $��0q�5D      
�������$��18(�5F      $��Right=�5G      ��5B      �� *CloseCommand+5G      5H      5I      �� 5L   
   $��1��6   $��	18,0,62,0q�6"   $��Center=�65   .+�5M      �� 
 �� Orientation$
 
Horizontal=�6   $��Stretch=�62   $
��0,10q�6P   .+�5N      �� $��Montant à payer :��6   #��[ 65   $��18(�6a   $	��200��6o   �� 5O      $��15(�5P       $
��Bold"�5Q       ���#10B981$	��100��5R   5   ��5O      �� *TotalTicket+ �� StringFormat$ 	{0:F3} DT��5R   5   5S      �� 5U      $
 
Horizontal=�6   $��Stretch=�62   $
��0,10q�6P   .+�5V      �� $��Montant reçu :��6   #��[ 62   $��18(�6^   $	��200��6l   �� 5W      $��22(�5X   
   $��10q�6   $	��150��6%   ��5W      �� *MontantRecu+ �� UpdateSourceTrigger$ PropertyChanged=�5X   %   5Y      5\      �� 5_   
   $��2��6   $
 
Horizontal=�6"   $��Center=�6;   $
��0,20q�6X   .+�5`      �� -   5        $��Annuler��5`      #��[ 5a      #��[ 5b      
�������$	��150��5e      ��5d      �� *CancelCommand+5e      �� 5f      $��Valider le paiement��6   #��[ 5g      #��[ 5h      
�������$	��200��5k      ��5j      �� *ValidatePaymentCommand+5k      5l      5m      5n      