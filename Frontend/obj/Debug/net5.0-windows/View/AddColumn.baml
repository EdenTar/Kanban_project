   M S B A M L   `   `   `  ����R  NIntroSE.Kanban.Frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntroSE.Kanban.Frontend.ViewIntroSE.Kanban.Frontend.View  5   	   ,    &IntroSE.Kanban.Frontend.View.AddColumn   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntroSE.Kanban.Frontend.View     5   	     � Title$  	AddColumn��5   	   $	��450��6   $	��800��6(   #��[B�64   .��5	      �  � ColumnDefinitions 5
   
   �� 5      �� 5      �� 5       � RowDefinitions 5   
   �� 5      $	��81* �6   �� 5      $	��74* �6   �� 5      $	��87* �6   �� 5      $	��78* �6   �� 5      $	��78* �6   �� 5      $	��35* �6   .+�5   
   �� $��
New Column��6   $��1��6%   $��Center=�65   $��1��6S   $	��Top=�6`   $��30(�6x   �� 5   
   $��Column��6   $
��Left=�6!   $��	21,14,0,0q�6<   $	��Top=�6O   �� 5   
   $��Name:��6   $
��Left=�6    $��	162,0,0,0q�6;   $��2��6N   �� 5   
   $��Ordinal:��6   $
��Left=�6#   $��	166,0,0,0q�6>   $��3��6Q   �� 5   
   -   5        $��Apply��5      $��1��6!   $��Center=�61   $��0,20,0,0q�6N   $��2��6`   $��4��6q   $	��Top=�6~   6�   �� 5   
   $��1��6   $��2��6#    �� TextWrapping$
 Wrap=�6[   $��Center=�6p   ��60   �� *Name+ �� Mode$ OneWayToSource=�6p   �� 5   
   $��1��6   $��3��6#   $
 Wrap=�6^   $��Center=�6s   ��60   �� *	Ordinal+$ OneWayToSource=�6s   �� 5    
   -   5        $
��Back��5       $��2��6    $
��Left=�60   $��
134,44,0,0q�6K   $��2��6_   $��4��6p   $	��Top=�6}   $��40��6�   $	��100��6�   6�   5!      5"      