   M S B A M L   `   `   `  ����K  Gcourse_proj_5sem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:course_proj_5semcourse_proj_5sem  5          course_proj_5sem.Settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      0localclr-namespace:course_proj_5sem     5      X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5        N� Title$  Settings��5
       � 	Resources 5         MyControlInputBox �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value �� RelativeSource	 �� Path  validationFailed
 �� Orientation �� CornerRadius��%�  &	        &	 �     �� 5   
   " ��6*    5      P� 5       �@=�HasError . . �5      ��  �@�ToolTip 5      ��  �� *Self+$)	 #(Validation.Errors)[0].ErrorContent��� 5       � ��
 �  ��63   5      5      �� 5   
   .R�5      �� $
 
Horizontal=�6   .+�5      �� 
���  ��$��2q�6+   $ 5��6?   ._�5      �� 5      �� 5      
���  ��$��26(�6-   $
��Bold"�6;   !6P   5      5      5      .��5       � -   5        $��	main_grid��5       .+�5!   
   �� -   5        $#��Изменить пароль��5!      $
��Left=�6+   $��52��6F   $��
470,92,0,0q�6R   $	��Top=�6f   $	��187��6~   6�   �� 5"   
   ���	#FFEEE8E1$��1q�6)   $
��Left=�6=   $	��212��6X   $	��Top=�6e   $	��804��6}   �� 5#   
   $#��Изменить пароль��6   $
��Left=�6*   $��32��6E   $��	32,10,0,0q�6Q   $	��Top=�6d   $	��198��6|   ,  &Xceed.Wpf.Toolkit.WatermarkPasswordBox  5$   
   -   5        $	��pas��5$   $   $��Center=�6/   $��35��6L   $��32,47,527,0q�6X   $	��Top=�6m   $	��240��6�   $��Center=�6�   .��6�   (  "Xceed.Wpf.Toolkit.WatermarkTextBox   	Watermark 5%      �� 5&      $
 
Horizontal=�6   .+�5'      �� 
���   �$��Center=�63   $��Старый пароль��6N   $��4,0,0,0q�6c   5(      .��5)   3   5*        5+   
   -   5        $
��pas1��5+   $   $��Center=�60   $��35��6M   $��32,154,527,0q�6Y   $	��Top=�6o   $	��240��6�   $��Center=�6�   .��6�    5,      �� 5-      $
 
Horizontal=�6   .+�5.      �� 
���   �$��Center=�63   $0��*Повторите новый пароль��6N   $��4,0,0,0q�6l   5/      .��50   3   51        52   
   -   5        $
��pas2��52   $   $��Center=�60   $��35��6M   $��32,100,527,0q�6Y   $	��Top=�6o   $	��240��6�   $��Center=�6�   .��6�    53      �� 54      $
 
Horizontal=�6   .+�55      �� 
���   �$��Center=�63   $��Новый пароль��6N   $��4,0,0,0q�6b   56      .��57   3   58      #  Xceed.Wpf.Toolkit.ColorPicker  59   
   -   5        $	��tem��59   =   $	��Top=�6J   $��Center=�6b   $	��200��6�   $��250,235,349,0q�6�   5;      �� 5<   
   ���	#FFEEE8E1$��1q�6)   $
��Left=�6=   $��92��6X   $��	0,211,0,0q�6d   $	��Top=�6w   $	��804��6�   �� 5=   
   $ ��Изменить  тему��6   $
��Left=�6)   $��32��6D   $��
32,217,0,0q�6P   $	��Top=�6d   $	��198��6|   �� 5>   
   -   5        $!��Экспорт данных��5>      $
��Left=�6*   $��38��6E   $��
76,340,0,0q�6Q   $	��Top=�6e   $	��242��6}   6�   �� 5?   
   -   5        $��Импорт данных��5?      $
��Left=�6)   $��38��6D   $��433,340,0,0q�6P   $	��Top=�6e   $	��242��6}   6�   5B      5C      