   M S B A M L   `   `   `  ����E  ACaissePoly, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:CaissePoly
CaissePoly  5   	   ;"clr-namespace:CaissePoly.ViewModelCaissePoly.ViewModel  5   	   ?$clr-namespace:CaissePoly.ComponnentsCaissePoly.Componnents  5   	       CaissePoly.FicheArticle   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:CaissePoly     5   	   8	viewmodel"clr-namespace:CaissePoly.ViewModel     5   	   ;
components$clr-namespace:CaissePoly.Componnents     5   	     � Title$  FicheArticle��5
   	   $	��700��5   	   $
��1302��5       � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	    � 
ResizeMode$ NoResize=�5       � AllowsTransparency$
 True��5      
������ .��5      � ���	#FFFDF1DC � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5!   
   ,   &CaissePoly.Componnents.BarreNavigation  $��0��6%   �� 5$   
   $��1��6   ���#A9D0F5 �� Padding$ 10q�63   ._�5%      �� $��FICHE ARTICLES��6   $��18(�6.   $
��Bold"�6<   ���#2C3E50$��Center=�5&   )   $��Center=�5'       �� TextAlignment$ Center=�61   5(      � 5+   
   $��2��6   $��10q�6    � ColumnDefinitions 5,      �� 5-      $��2* �6#   �� 5.      $��3* �6#   .+�52      �� $��0��6   $
��Auto=�6+   $��10q�6N   .��53      �� 	 � 	Resources	 54      
 �� 
TargetType �� Property �� Value��%�  '��       '�� �     '�� [    �� 55      "
 ��6    .�56      ��  � ��$
 Bold"�6;   �� 57       � ��$ 0,10,0,2q�67   �� 58       � �� �#2C3E506;   59      �� 5:      "
 ��6    .�5;      ��  � ��$ 0,0,0,5q�67   �� 5<       � ��$ 5q�68   �� 5=       � ��$ 1q�6@   �� 5>       � �� �#BDC3C76<   5?      �� 5@      "
 ��6    .�5A      ��  � ��$ 0,5,0,15q�67   5B      5C      .+�5E      �� $
��Code��6   �� 5F      ��6   �� *SelectedArticle.idA+{�5G      �� *DataContext.IsCodeEnabled+ �� RelativeSource ��  �� AncestorType
 Window�� 5I      $��Code à barre��6   �� 5J      ��6   �� *SelectedArticle.codeabare+�� 5L      $��Désignation��6   �� 5M      ��6   �� *SelectedArticle.designation+�� 5O      $��Unité��6   �� 5P      $	��DT ��6   �� 5R      $��
ID Famille��6   �� 5S      ��6   �� *SelectedArticle.idF+�� 5U      $
��Prix��6   �� 5V      ��6   �� *SelectedArticle.prixunitaire+�� 5X      $	��TVA��6   �� 5Y      $	��18%��6   �� 5[      $��Stock��6   �� 5\      ��6   �� *SelectedArticle.quantiteStock+�� 5^      $��Quantité Du Vente��6   �� 5_      ��6   �� *SelectedArticle.quantiteVente+5`      5a      � 5d      $��1��6   $��10q�6#    5e      �� 5f      $
��Auto �6$   �� 5g      $��* �6$   .+�5k      �� $��0��6    �� Orientation$ 
Horizontal=�6*   $��Stretch=�6C   $��0,0,0,10q�6a   .+�5l      �� $	��200��6   $��0,0,10,0q�6*   $��5q�5n      ��5m      �� *
SearchText+ �� UpdateSourceTrigger$ PropertyChanged=�5n      �� 5o      $
��🔍��6    � ToolTip$  Afficher tous les articles��5q      $��0,0,10,0q�5r      $	��120��5s      $��30��5t      ��5p      �� *SearchCommand+5t      �� 5u      $��14(�5v       ���#64748B$��Center=�5x       ��5u       �� *FilteredArticles.Count+ �� StringFormat$ {0} articles��5x       5y      �� 5|      $��1��6   $��0q�5      $��Stretch=�5�      $��Stretch=�5�      $��0q�5�      $��Disabled=�5�      ��5}      �� *
Articles+ �� SelectedItem 5~      �� *SelectedArticle+ �� Mode$ TwoWay=�	 5�       �� Triggers P� Property P� Value��%  '��       �� 5�      "
 ��6    .�5�      ��  � ��$ 8q�68   �� 5�       � ��$ 0,0,0,4q�67   �� 5�       � ��
 �����6;   �� 5�       � �� �#E2E8F06<   �� 5�       � ��$ 1q�6@   �� 5�       � ��$ Stretch=�6K    5�      P� 5�   "    �@J�IsMouseOver$
 True��6A   . �5�   &   ��  � �� �#F8FAFC6C   5�   #   P� 5�   "    �@��
IsSelected$
 True��6@   . �5�   &   ��  � �� �#EFF6FF6C   �� 5�   &    � �� �#3B82F66D   5�   #   5�      5�       �� View 5�      �� 5�      .P�5�      �� $
��Code��6-    �� Width$ 80��6;    �� CellTemplate 5�   "   �� 5�   &   .R�5�   *   �� $��Center=�6I   ��64   �� *idA+6I   5�   '   5�      �� 5�      $��Désignation��6-   $	 200��6B    5�   "   �� 5�   &   .R�5�   *   �� $
��Wrap=�6Q   $��Center=�6e   ��64   �� *designation+6e   5�   '   5�      �� 5�      $
��Prix��6-   $ 80��6;    5�   "   �� 5�   &   .R�5�   *   �� $��Right=�6i   $��Center=�6�   ��64   �� *prixunitaire+$ {0:N2}��6�   5�   '   5�      �� 5�      $��Stock��6-   $ 80��6<    5�   "   �� 5�   &   .R�5�   *   �� $��Right=�6S   $��Center=�6o   ��64   �� *quantiteStock+6o   5�   '   5�      5�      5�      5�      5�      �� 5�   
   $��3��6   ���#ECF0F1$ 10q�63   $��0,1,0,0q�6@   ���#BDC3C7._�5�      �� $ 
Horizontal=�6   $��Center=�62   .+�5�      �� $��Nouveau��6   $��5q�6+   $	��120��66   $��35��6B   ���#3498DB
�������$
��Bold"�5�   A   $��0q�6S   ��5�      �� *NewArticleCommand+5�   S   �� 5�      $��Sauvegarder��6   $��5q�6/   $	��120��6:   $��35��6F   ���#2ECC71
�������$
��Bold"�5�   A   $��0q�6S   ��5�      �� *SaveArticleCommand+5�   S   �� 5�      $��	Supprimer��6   $��5q�6-   $	��120��68   $��35��6D   ���#E74C3C
�������$
��Bold"�5�   A   $��0q�6S   ��5�      �� *RemoveSelectedArticleCommand+5�   S   �� 5�      $��Retour��6   $��5q�6*   $	��120��65   $��35��6A   ���#95A5A6
�������$
��Bold"�5�   A   $��0q�6S   ��5�      �� *CloseCommand+5�   S   5�      5�      5�      5�      