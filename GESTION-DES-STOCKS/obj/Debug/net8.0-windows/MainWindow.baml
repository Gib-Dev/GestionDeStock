   M S B A M L   `   `   `  ����  GESTION-DES-STOCKS#    GESTION_DES_STOCKS.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$"  Gestion de Stock - Connexion��5   	   $	��500��5   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5
   	    � WindowStyle$
 None=�5   	   .��5      � ���#6a00f4.+�5   
   �� 
������� �� CornerRadius$ 15��6$    �� Padding$ 0q�66   $��20q�6B    J� SnapsToDevicePixels$
 True��6N    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#   	   ShadowDepth$	 0Q�63   
   Opacity$	
 0.5Q�6C   ._�5      �  � ColumnDefinitions 5      �� 5      $��* �6'   �� 5      $
��1.5* �6'   .+�5      �� $ 	15 0 0 15��6   ���#6a00f4._�5      � .+�5      �� $��Center=�6%   $��Center=�6@   .+�5      �� $,��&pack://application:,,,/Images/logo.png��6$   $	��100��6T   $	��100��6`   $��0,0,0,20q�6m   �� 5      $��Hello!��6(   $��36(�66   
�������$
��Bold"�6W    �� TextAlignment$ Center=�6i   $
��0,10q�6�   �� 5       $��Good Morning��6(   $��24(�6<   
�������$ Center=�6]   $	��0,5q�6t   5!      5"      5#      �� 5$      .+�5&      �  � Points# �    �� �� ;X �� �����#d5e8c0 J� Opacity$	 0.3Q�6L   � 5'      ' �    �� �� ���� �������#d5e8c0$	 0.3Q�6N   �� 5(      .+�5*      � # �    �� �� ;X �� �����#d5e8c0$	 0.3Q�6P   � 5+      ' �    �� �� ���� �������#d5e8c0$	 0.3Q�6R   � 5,      ' �   ������ �� �� ���,���#d5e8c0$	 0.3Q�6P   A� 5-       �� Left$	 230��6"   
 �� Top$	 445��64   $��40��6E   $��40��6P   ���#d5e8c0$	 0.3Q�6k   A� 5.      $ 45��6"   $	 350��63   $��30��6D   $��30��6O   ���#d5e8c0$	 0.3Q�6j   A� 5/      $ 55��6"   $ 60��63   $��40��6C   $��20��6N   ���#d5e8c0$	 0.3Q�6i   50      � 51      ' �   ������ �� �� ���,���#d5e8c0$	 0.3Q�6L   A� 52      $	 230��6   $	 445��60   $��40��6A   $��40��6L   ���#d5e8c0$	 0.3Q�6g   A� 53      $ 45��6   $	 350��6/   $��30��6@   $��30��6K   ���#d5e8c0$	 0.3Q�6f   A� 54      $ 55��6   $ 60��6/   $��40��6?   $��20��6J   ���#d5e8c0$	 0.3Q�6e   55      �� 58      
�������$��1��6,   $ 	0 15 15 0��6<   $ 40q�6U   ._�59      �� $��Center=�6!   .+�5:      �� $��Login your account��6$   $��20(�6>   ���#6a00f4$
��Bold"�6a   $ Center=�6s   $��0,0,0,20q�6�   �� 5<      $��Username��6    $
��Bold"�63   
�������6E   �� 5=      -   5         ��Name$ NomUser��5=   4   $��0 0 0 10q�6"   $	��250��6E   $��30��6Q   $��5q�6]   �� 5?      $��Password��6    $
��Bold"�63   
�������6E   G� 5@      -   5         G�Name$ 
MotDePasse��5@   &   $	��250��6:   $��30��6F   $��5q�6R   �� 5B      $��Right=�6$   
�������$��0,5,0,0q�6R   �� 5C      -   5        .�5C   G   Forgot Password?6Y   5D      �� 5F      -   5        $��Login��5F   !   $	��150��61   $��0,20,0,0q�6=   $��Center=�6g   ��5G      �� 5H   "    �� 
TargetType" ��6(   .�5I   &   ��  �� Property � �� �� Value �#6a00f46C   �� 5J   &    � ��
 �����6C   �� 5K   &    � ��$
 Bold"�6C   �� 5L   &    �@�Cursor$
 Hand��6?   �� 5M   &    � ��$ 0q�6H   �� 5N   &    � ��$
 10,5q�6@    �� Triggers 5O   &   P� 5P   *    P� Property �@J�IsMouseOver P� Value$
 True��6I   . �5Q   .   ��  � �� �#5a00d46K   5R   +   5T   #   5V      �� 5Y      $��Center=�6$   $��0,20,0,0q�6A   
��������� 5Z      $�Don't have an account? ��6"    �� 5[      -   5        .�5[   B   Create Account6R   5\      �� 5^      -   5         ��Name$ ErreurMessage��5^       $�� ��67   
���  ��$��Center=�6S   $ 0Q�6p   $��0,10,0,0q�6|   5_      5`      5a      5b      �� 5e   
   -   5        $��X��5e      $��30��6   $��30��6(   $��Right=�64   $	��Top=�6P   $��	0,10,10,0q�6h   
������ 
������ 
�������$
��Bold"�5f   V    � Cursor$
 Hand��6h   ��5g      �� 5h      " ��6   .�5i      ��  � �� 5j      �� 5k       �� 
TargetType" ��6.   .R�5l   "   � .+�5m   &   A�  A�Name$ ellipse��6A   
������ �� 5n   &   $��Center=�67   $��Center=�6T   5o   #    �� Triggers 5p   "   P� 5q   &    �@J�IsMouseOver$
 True��6E   . �5r   *   ��  �� 
TargetName$ ellipse��61    � �� �#FFCCCC6V   �� 5s   *    � ��
 �  ��6G   5t   '   5v      5x      5y      5{      5|      5}      