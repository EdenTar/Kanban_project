   M S B A M L   `   `   `  ����R  NIntroSE.Kanban.Frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntroSE.Kanban.Frontend.ViewIntroSE.Kanban.Frontend.View  5   	   +    %IntroSE.Kanban.Frontend.View.Register   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntroSE.Kanban.Frontend.View     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	      MaterialDesignBody#���   5	   	   $��Regular"�5
   	   $��13(�5   	   &   System.Windows.Media.TextOptions    TextFormattingMode$  Ideal=�5   	      TextRenderingMode$
 Auto=�5   	     MaterialDesignPaper#���  5   	     MaterialDesignFont#���  5   	    � Title$ Register��5   	   $	��450��6   $	��800��6'   .��5      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FF1B263C��6    � Offset$
 0.27Q�61   � 5      $ 	#FF4ABDBB��6   $ 0.756Q�61   5      .+�5   
   ��  �� Orientation$ Vertical=�6   .+�5      �� $��Register��6   $��2��6'   $
��Left=�67   $��Segoe UI Black)�6R   $��40(�6n   $��73��6|   $	��202��6�   ���	#FFFAF0F06�   � 5      $	��830��6   $	��201��6    � ColumnDefinitions 5      �� 5      $
��129* �6'   �� 5      $
��283* �6'   �� 5       $
��311* �6'   �� 5!      $
��107* �6'   	 � RowDefinitions	 5#      �� 5$      $
��Auto �6$   �� 5%      $	��18* �6$   �� 5&      $	��11* �6$   .+�5(      �� $��Enter email:��6   $��Center=�6/   ���	#FFFAF0F0$��Segoe UI Black)�6c   $��30(�6   $��1��6�   $��52��6�   $	��203��6�   $
��Bold"�6�   $��Normal$�6�   
 J� RenderTransformOrigin$
 0.447,-0.225&�6�   $��0,19,0,0q�6�   $	��Top=�6  �� 5)      $��Enter password:��6   $��1��62   $��Center=�6B   ���	#FFFAF0F0$��52��6v   $��1��6�   $	��260��6�   $��Normal"�6�   $��Segoe UI Black)�6�   $��30(�6�   �� 5*      $��2��6>   $��Center=�6N   $��32��6k    �� TextWrapping$
 Wrap=�6w   $	��292��6�   ���	#FFFAF0F0���	#FF1F939F$��17(�6�   $��0,34,0,0q�6�   ��6   �� *Email+ �� Mode$ TwoWay=�6�   �� 5+      $��2��6A   $��Center=�6Q   $��32��6n   $
 Wrap=�6z   $	��292��6�   ���	#FFFAF0F0���	#FF1F939F$��17(�6�   $��1��6�   $��0,30,0,0q�6�   ��6   �� *
Password+$ TwoWay=�6�   �� 5,      
���  ��$��1��6Y   $��Center=�6i   $��0,5,0,0q�6�   $��2��6�   $��Stretch=�6�   $��2��6�   $	��594��6�   $��17(�6�   ��6   ��  �� Path$ Message�$ OneWay=�6�   5-      � 5.      $	��803��6   $��67��6    5/      �� 50      $
��321* �6'   �� 51      $	��25* �6'   �� 52      $
��457* �6'   .+�54      �� -   5        $��Register��54      $
��Left=�6,   $��52��6G   $��	19,10,0,0q�6S   $	��Top=�6f   $	��124��6~   $
��Bold"�6�   $��Segoe UI Black)�6�   $��22(�6�   ���	#FFFAF0F0$��2��6�   $��1��6  ��55      �� 56      $ 0.5,1&�6.   $ 0.5,0&�6?   .��57      � $ Black��6+   � 58      $ 	#FF1F939F��6+   $ 1Q�6=   59      5;      5=      � 5>      $	��803��6   $	��100��6   .+�5?      �� -   5        $��Login��5?      $��51��6)   $	��122��65   $
��Bold"�6A   $��Segoe UI Black)�6S   $��22(�6o   ���	#FFFAF0F0$��661,15,20,34q�6�   ��5@      �� 5A      $ 0.5,1&�6.   $ 0.5,0&�6?   .��5B      � $ Black��6+   � 5C      $ 	#FF1F939F��6+   $ 1Q�6=   5D      5F      5G      5H      5I      5J      