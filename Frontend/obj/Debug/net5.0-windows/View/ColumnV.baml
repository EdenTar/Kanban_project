   M S B A M L   `   `   `  ����R  NIntroSE.Kanban.Frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntroSE.Kanban.Frontend.ViewIntroSE.Kanban.Frontend.View  5   	   *    $IntroSE.Kanban.Frontend.View.ColumnV   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntroSE.Kanban.Frontend.View     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  ColumnV��5	   	   $	��450��6   $	��800��6&   #��[B�62   .��5
      �  � RowDefinitions 5   
   �� 5      $
��0.2* �6   6,   �� 5      $
��0.7* �6   6,   �� 5      $
��0.2* �6   6,   .+�5   
   �� $��0��6   $��Column��6   $
��Left=�6.   $��20,0,0,0q�6I   6]   �� 5   
   $��0��6   $��40(�6H   $��Center=�6V   ��6   ��  �� Path$
 Name� �� Mode$ OneWay=�6u   �� 5   
   $��120,40q�6   $��1��6!    �� CornerRadius$ 30��6.   
���   �._�5      �  5      �� 5      6&   �� 5      6&    � ColumnDefinitions 5      �� 5      $
��0.6* �6'   66   �� 5      6)   �� 5      $
��0.4* �6'   66   .+�5      �� $
��Name��6   $��Center=�6'   $��Center=�6D   6a   �� 5      $��2��6   $��Limit��6%   $��Center=�65   $��Center=�6R   6o   �� 5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Enter name here...��6B   $��20(�6v   $��1��6�   $
��Auto��6�   $
��Auto��6�   $��Center=�6�    %  !MaterialDesignFloatingHintTextBox#��[  5       
���   �
�������$��
10,0,10,20q�5#      ��5      �� $
 Name�$ TwoWay=�5#      �� 5$      $ Enter limit here...��6C   $��20(�6x   $��1��6�   $��1��6�   $
��Auto��6�   $
��Auto��6�   $��Center=�6�   #��[  5%      
���   �
�������$��
10,0,10,20q�5(      ��5$      �� $ Limit�$ TwoWay=�5(      �� 5)      -   5        $��2��5)   +   $
��Auto��6;   $
��Auto��6H   $
��5,20q�6V   .��5*      ��  �� Orientation$ 
Horizontal=�6!   .+�5+      �� $��Apply��6    62   '  !MaterialDesignThemes.Wpf.PackIcon  5,         Kind$ ContentSave=�62   $��Center=�6E   $��25��6`   $��25��6k   5-      5.      �� 5/      -   5        $��1��5/   ,   $��2��69   $
��Auto��6I   $
��Auto��6V   $
��5,20q�6d   .��50      �� $ 
Horizontal=�6!   .+�51      �� $��Apply��6    62     52      $ ContentSave=�62   $��Center=�6E   $��25��6`   $��25��6k   53      54      55      56      �� 57   
   -   5        $��2��57      $��Center=�6   
���   �.��58      �� $ 
Horizontal=�6   .+�59      �� $
��Back��6   6)     5:      $ KeyboardReturn=�6*   $��Center=�6@   $��25��6[   $��25��6f   5;      5<      5=      5>      