   M S B A M L   `   `   `  ����  Reservation.UI&     Reservation.UI.BookingDetailView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .��5      �   � RowDefinitions  5	   
   �� 5
      $
��Auto �6   �� 5      $��* �6   �� 5      $��60 �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $
��Left=�6   $��10,0,0,0q�6/   $
��Auto��6A   $��Booking Details��6N   $
��Bold"�6h   ���	#FF373737$��24(�6�   $��Corbel)�6�   5      &   System.Windows.Controls.DataGrid  5   
      AutoGenerateColumns .  $��1��6�   ��6   �� *BookingDetails+ �� SelectedItem 6:   �� *SelectedBookingDetail+   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 90 6)      HeaderTemplate 5      �� 5      .R�5      �� $��Table Id��6(   $
��Wrap=�68   5         CellTemplate 5      �� 5      .R�5      �� $
��Wrap=�6A   ��6(   �� *	TableId+6A   5      5        5      $	 150 6)    5       �� 5!      .R�5"      �� $��Checkin Time��6(   $
��Wrap=�6<   5#       5%      �� 5&      .R�5'      �� $
��Wrap=�6E   ��6(   �� *CheckInTime+6E   5(      5*        5+      $	 200 6)    5,      �� 5-      .R�5.      �� $��Customer Name��6(   $
��Wrap=�6=   5/       51      �� 52      .R�53      �� $
��Wrap=�6R   ��6(   �� *CustomerDetail.FirstName+6R   54      56        58      $	 150 6)    59      �� 5:      .R�5;      �� $��Mobile #��6(   $
��Wrap=�68   5<       5>      �� 5?      .R�5@      �� $
��Wrap=�6U   ��6(   �� *CustomerDetail.MobileNumber+6U   5A      5C      5F      �� 5G   
   $��2��6   .+�5H      �� $
��View��6   $	��100��6$   ��60   �� *ViewCommand+5I      5J      5K      