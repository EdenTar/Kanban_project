   M S B A M L   `   `   `  ����R  NIntroSE.Kanban.Frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntroSE.Kanban.Frontend.ViewIntroSE.Kanban.Frontend.View  5   	   .    (IntroSE.Kanban.Frontend.View.PopupErrorV   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntroSE.Kanban.Frontend.View     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$  PopupErrorV��5	   	   $	��200��6   $	��400��6*   
���  ��.��5
      �  � RowDefinitions 5   
   �� 5      6   �� 5      6   .+�5   
   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ AlertCircle=�6"   $	��Top=�65   $
��Left=�6M   
�������$��30��6{   $��30��6�   �� 5   
   
���  ��
���������6   ��  �� Path$ ErrorMsg� �� Mode$ OneTime=�6f   �� 5   
   -   5        $��1��5      $��Accept��6   
���  ��
�������$��4q�6c   6y   5      5      