   M S B A M L   `   `   `  ����  GESTION-DES-STOCKS(    "GESTION_DES_STOCKS.ProductsControl   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Gestion des Produits��5   	   $	��600��6&   $	��800��63    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � ���#f4f4f9$��20q�6    .+�5
   
   ��  �� Orientation$ Vertical=�6   $��0,0,0,10q�6,   .+�5      �� $��Gestion des Produits��6   $��24(�5      $
��Bold"�6&   ���#3e3a60$��Center=�5      �� 5      $.��(Ajoutez, modifiez et gérez vos produits��6   $��14(�5      
�������$��Center=�5      5      � 5   
   $��0,20,0,0q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    .+�5      �� $ 
Horizontal=�6   $��0��62   $��Center=�6?   $��	0,10,0,10q�6\   .+�5      � .+�5      �� -   5         ��Name$ SearchProductBox��5   6   $	��200��6   $��30��6*   $��5q�6P   $��0,0,10,0q�6\   �� 5       $��Rechercher un produit��6     J� IsHitTestVisible$ False��5!       
�������$��5,0,0,0q�5#       $��Center=�5$       ��5%      �� 5&       �� 
TargetType" ��6$   .�5'   "   ��  �� Property � y�	 �� Value$	 	Collapsed=�6?   
 �� Triggers
 5(   "   �� 5)   &    �� Binding 62   �� *Text+ �� ElementName$ SearchProductBox�� �� Value  6i   .a�5*   *   ��  � y�$	 Visible=�6G   5+   '   5-      5/      50      �� 51      -   5        $��
Rechercher��51      $	��100��6.   $��30��6:   ��52      �� 53      " ��6    .�54      ��  � ��	 �#6a00f46;   �� 55       � ��
	 �����6;   �� 56       � ��$
	 Bold"�6;   �� 57       �@�Cursor$
	 Hand��67   �� 58       � ��$	 0q�6@   �� 59       � ��$
	 10,5q�68   
 5:      P� 5;   "    P� Property �@J�IsMouseOver P� Value$
 True��6A   . �5<   &   ��  � ��	 �#5a00d46C   5=   #   5?      5A      5B      �� 5E      $��1��6   
������� �� CornerRadius$ 10��65    �� Padding$ 10q�6G   $��10q�6T   ._�5F      �� .��5G      &   System.Windows.Controls.DataGrid  -   5          Name$ ProductDataGrid��5G         AutoGenerateColumns$ False��68      CanUserAddRows$ False��6T   $��0,0,0,10q�6k      Columns 5H      0  *System.Windows.Controls.DataGridTextColumn  5I      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��61      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6=   �� *	ProductID+6[     5J      $ Nom du Produit��61   $ 2* 6i    6I   �� *ProductName+6i     5K      $ Description��61   $ 3* 6f    6F   �� *Description+6f     5L      $
 Prix��61   $ * 6Y    6?   �� *Price+6Y     5M      $ Quantité en Stock��61   $ * 6n    6L   �� *StockQuantity+6n   5O      5P      5Q      �� 5T      $��2��6   $ 
Horizontal=�6&   $��Center=�6?   $��0,20,0,0q�6\   .+�5U      �� -   5        $��Ajouter��5U      $	��100��6+   $��35��67   $
��10,0q�6C   ��5V      �� 5W      " ��6    .�5X      ��  � ��	 �#00bfa56;   �� 5Y       � ��
	 �����6;   �� 5Z       � ��$
	 Bold"�6;   �� 5[       �@�Cursor$
	 Hand��67   �� 5\       � ��$	 0q�6@   �� 5]       � ��$
	 10,5q�68   
 5^      P� 5_   "    �@J�IsMouseOver$
 True��6A   . �5`   &   ��  � ��	 �#0096886C   5a   #   5c      5e      �� 5g      -   5        $��Modifier��5g      $	��100��6,   $��35��68   $
��10,0q�6D   ��5h      �� 5i      " ��6    .�5j      ��  � ��	 �#ffa7266;   �� 5k       � ��
	 �����6;   �� 5l       � ��$
	 Bold"�6;   �� 5m       �@�Cursor$
	 Hand��67   �� 5n       � ��$	 0q�6@   �� 5o       � ��$
	 10,5q�68   
 5p      P� 5q   "    �@J�IsMouseOver$
 True��6A   . �5r   &   ��  � ��	 �#fb8c006C   5s   #   5u      5w      5x      5y      5z      5{      