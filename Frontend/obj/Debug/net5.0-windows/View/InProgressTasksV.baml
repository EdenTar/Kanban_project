   M S B A M L   `   `   `  ����R  NIntroSE.Kanban.Frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntroSE.Kanban.Frontend.ViewIntroSE.Kanban.Frontend.View  5   	   3    -IntroSE.Kanban.Frontend.View.InProgressTasksV   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntroSE.Kanban.Frontend.View     5   	     � Title$  InProgressTasksV��5   	   $	��450��6"   $	��500��6/   #��[B�6<   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��41* �6   �� 5      $	��51* �6   �� 5      $	��51* �6   �� 5      $
��223* �6   �� 5      $	��62* �6   .+�5   
   �� $
��Task��6   $��37��6   �� 5   
   $��In progress tasks��6   $��30(�6,   $��Center=�6:   $��50��6W   $	��262��6c   $��0,32,0,0q�6o   $��2��6�   $	��Top=�6�   �� 5   
   
�������$��3��6$   $	��278��61    �� CornerRadius$ 20��6=   
���   �$��4q�6c   6y   �� 5   
   -   5        
�������$��	0,10,0,10q�5   %   $��3��6�   $��Center=�6�   $	��262��6�   $��Center=�6�   ��68   �� *InProgressTasksM.Tasks+ �� SelectedItem 6g   �� *SelectedTask+ �� Mode$ TwoWay=���5      �� 5      .R�5      �� $ 10��6   $��4q�6/   
�������$��0,2,0,2q�6�   ��6C   ��  �� Path$ BorderColor�$ OneWay=�._�5      �  5      �� 5   "   $��5* �60   6>   �� 5   "   $��4* �60   6>   �� 5   "   $��4* �60   6>   .+�5      �� $��0��6%   $��0q�62   
�������$ 	10,10,0,0��6Y   6t   �� 5       $��0��6$   $��Center=�61   $��14(�6N   $��0q�6\   
���   ���6{   �� $ Title�$ OneTime=�6�   � 5!      $��1��6*   
�������$��12��5"   >    � Value 5!   7   �� *TimeTillDueDate+$ OneWay=���6z   �� *DueDateColor+$ OneWay=�5"   >   � 5#      $��2��6#    � ColumnDefinitions 5$   "   �� 5%   &   �� 5&   &   .+�5(   "   �� $��12(�6)   $��0q�67   
���   ���6V   �� $ Assignee�$ OneTime=�6�   �� 5)   "   $��1��6)   $��12(�69   
���   �$��0q�6Z   ��6f   �� $ DueDate�$ OneTime=�6�   5*      5+      5,      5-      5/      �� 50   
   -   5        $
��Back��50      $��35��6!   $��95��6-   $��4��6K   $��Right=�6X   $��Bottom=�6u   $��0,0,6,6q�6�   �� 51   
   $��Center=�6;   $��2��6X   $��20(�6f   ��6   �� $ Email�$ OneWay=�6f   �� 52   
   -   5        $
��Sort��52      $��Center=�6    $��0,10,0,0q�6=   $��4��6O   $	��Top=�6\   6t   55      56      