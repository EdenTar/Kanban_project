   M S B A M L   `   `   `  ����R  NIntroSE.Kanban.Frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntroSE.Kanban.Frontend.ViewIntroSE.Kanban.Frontend.View  5   	   ,    &IntroSE.Kanban.Frontend.View.ViewTaskV   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntroSE.Kanban.Frontend.View     5   	     � Title$  	ViewTaskV��5   	   $	��600��6   $	��800��6(   #��[B�65   .��5	      �  � ColumnDefinitions 5
   
   �� 5      $	��24* �6   �� 5      $	��51* �6   �� 5      $	��25* �6    � RowDefinitions 5   
   �� 5      $
��0.8* �6   �� 5      $
��0.5* �6   �� 5      $
��0.8* �6   �� 5      $��1* �6   �� 5      $
��0.8* �6   �� 5      $
��0.8* �6   �� 5      $��1* �6   .+�5   
   �� $��Center=�67   $��30(�6U   $��1��6c   $��1��6p   $��Center=�6�   ��6   �� *Title+ �� Mode$ OneWay=�6�   �� 5   
   $
��Task��6   $
��Left=�6   $��0��6<   $��	18,10,0,0q�6I   $	��Top=�6\   �� 5   
   $	��ID:��6   $
��Left=�6   $��	21,45,0,0q�6:   $��36��6M   $��39��6Y   $��15(�6d   $	��Top=�6r   �� 5   
   $��Title:��6   $
��Left=�6!   $��2��6<   $��30,0,0,0q�6I   $��37��6[   $��60��6g   �� 5   
   $��Description:��6   $
��Left=�6'   $��3��6C   $��43��6P   $	��146��6\    J� RenderTransformOrigin$ 0.687,0.731&�6h   $��	30,23,0,0q�6�   $	��Top=�6�   �� 5   
   $��	Due date:��6   $
��Left=�6$   $��4��6@   $��0��6M   $��48��6]   $	��112��6i   $��30,7,0,0q�6u   $	��Top=�6�   �� 5    
   $��	Assingee:��6   $
��Left=�6$   $��30,7,0,0q�6?   $��5��6R   $��0��6`   $��38��6p   $	��106��6|   $	��Top=�6�   �� 5!   
   $��Creation time:��6   $
��Left=�6)   $��	21,0,0,29q�6D   $��Bottom=�6W   $��15(�6r   $��2��6�   �� 5"   
   ���	#FFFDF2F2$��
53,46,79,0q�6L   $��15(�6`   $��34��6n   $	��Top=�6z   ��6   �� *Id+$ OneWay=�6z   �� 5#   
   ���	#FFFDF2F2$��136,75,300,0q�6U   $��15(�6k   $
��Left=�6z   $��29��6�   $��2��6�   $	��Top=�6�   $��2��6�   ��6   �� *CreationTime+$ OneWay=�6�   �� 5$   
   $��1��69   $��2��6I   $��40��6V   ���	#FF1F939F��6   �� *Title+$ OneWay=�6c   �� 5%   
   $��1��6?   $��3��6O   $��75��6\   ���	#FF1F939F��6   �� *Description+$ OneWay=�6i   �� 5&   
   $��1��6<   $��5��6L   $��40��6Y   ���	#FF1F939F��6   �� *
Assignee+$ OneWay=�6e   �� 5'   
   $��1��6;   $��4��6K   $��40��6X   ���	#FF1F939F��6   �� *	DueDate+$ OneWay=�6d   �� 5)   
   -   5        $
��Back��5)      $
��Left=�6    $��	136,0,0,0q�6;   $��8��6N   $��Center=�6[   $��45��6�   $	��140��6�   $��2��6�   $��1��6�   5*      5+      