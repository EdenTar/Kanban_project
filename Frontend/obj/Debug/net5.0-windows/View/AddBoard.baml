   M S B A M L   `   `   `  ����R  NIntroSE.Kanban.Frontend, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:IntroSE.Kanban.Frontend.ViewIntroSE.Kanban.Frontend.View  5   	   +    %IntroSE.Kanban.Frontend.View.AddBoard   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   <local*clr-namespace:IntroSE.Kanban.Frontend.View     5   	     � Title$  AddBoard��5   	   $	��450��6   $	��800��6'   .��5	      � .+�5
   
   �� $��	Add Board��6   $��1��6$   $��Center=�64   $��30(�6Q   $��0,58,0,0q�6_   $	��Top=�6q   $��50��6�   $	��168��6�   �� 5   
   $��Board��6   $
��Left=�6    $��	27,23,0,0q�6;   $	��Top=�6N   $��37��6f   $��69��6r   �� 5   
   $��Board name: ��6   $
��Left=�6'   $��52,0,0,0q�6B   $��1��6T   $��36��6a   $	��138��6m   �� 5   
   $��Email Of creator:��6   $
��Left=�6,   $��52,0,0,0q�6G   $��2��6Y   $��36��6f   $	��176��6r   �� 5   
   $��1��6   $��1��6#    �� TextWrapping$
 Wrap=�6`   $��45��6t   $��0,24,0,0q�6�   ��60   �� *	BoardName+ �� Mode$ OneWayToSource=�6�   �� 5   
   $��1��6   $��2��6#   $
 Wrap=�6c   $��45��6w   $��0,45,0,0q�6�   ��60   �� *CreatorEmail+$ OneWayToSource=�6�   �� 5   
   -   5        $��Apply��5      $��1��6!   $��Center=�61   $��0,19,0,0q�6N   $��3��6`   $	��Top=�6m   $��45��6�   $	��140��6�   6�   �� 5   
   -   5        $
��Back��5      $��2��6    $
��Left=�60   $��	64,59,0,0q�6K   $��3��6^   $	��Top=�6k   $��2��6�   $��40��6�   $	��100��6�   6�    � ColumnDefinitions 5   
   �� 5      $	��23* �6   �� 5      $	��37* �6   �� 5      $	��20* �6    � RowDefinitions 5   
   �� 5      $
��111* �6   �� 5      $	��84* �6   �� 5      $
��125* �6   �� 5      $	��89* �6   �� 5      $	��25* �6   5"      5#      