   M S B A M L   `   `   `  ���� U  QPresentationUI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35[>clr-namespace:System.Windows.Documents;assembly=PresentationUISystem.Windows.Documents   CYOA   CYOA.utilities.StoryTree   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     T	Documents>clr-namespace:System.Windows.Documents;assembly=PresentationUI       � 	Resources     brdChoiceStyle �� 
TargetType �� Property �� Value � Color � Offset �� Triggers P� Property P� Value + 'FlowDocumentPageViewerStyleRoundCorners	 �� 
ResourceId
 �� TypeInTargetAssembly;   5System.Windows.Documents.PresentationUIStyleResources-  'System.Windows.Input.KeyboardNavigation �� 
TargetType � Language
 J� Uid � ColumnDefinitions ��Name �� CornerRadius �� Orientation Y� CanGoToPreviousPage � ToolTip �� BasedOn ��Name �� ContentStringFormat �� ContentStringFormat J� RenderTransformOrigin �� Triggers �� 
TargetName �� ScaleY �� ScaleX �� RelativeSource Y� CanGoToNextPage .� CanDecreaseZoom  � LayoutTransform! �� Padding" (�Name# @� Center$ @� RadiusY% @� RadiusX& F�Name' �� EndPoint( �� 
StartPoint) �� Left
* �� Top+ �� EndPoint, �� 
StartPoint- .� ZoomIncrement. � LargeChange/ .� MaxZoom0 � Maximum1 .� MinZoom2 � Minimum3 � SmallChange4 �� TickFrequency5 � Value6 �� Mode7 � RowDefinitions8 m�Name9 m� Fill: m� 	Placement; J� Opacity< ��Name= ]�Name> ]� DecreaseRepeatButton? ]� IncreaseRepeatButton@ ]� ThumbA �� StrokeEndLineCapB �� StrokeDashCapC �� StrokeMiterLimitD �� StrokeLineJoinE �� RelativeTransformF T� XG T� YH �� AngleYI �� AngleXJ �� AngleK .� CanIncreaseZoomL   	IsTabStopM Y� IsMasterPageN \� 
PageNumberO   TabIndexP   TabNavigationQ \� DocumentPaginatorR ]� 
ConditionsS �� PropertyT �� Value��%Z+  &	        &	      'R� **    �� " ��.���  � �� �� ��� $ White��$ 0Q� P�  �@J�IsMouseOver . . ���  � ��
 ������� " .�.���  � ��
 ���� ��  �@�ContextMenu C� *�� $ 	 PUIFlowViewers_ContextMenu��"
  +��  � ��$ Center=���  � ��$ Center=���  �@ TabNavigation$ Local=���  � �� �� " .�.R�]� $ en-us���. $ DockPanel_2��.+�� 
������ $��Bottom=�$��Stretch=�$ Grid_23�� �� $��* ��� $
��Auto ��� $��* �.+��� $ PART_FindToolBarHost��$��3��$ 	Border_60��
�������$ 	0,0,20,20���� $��1��$��Center=�$ 
Horizontal=�$ StackPanel_1��$��Center=�.+��� $%��NavigationCommands.PreviousPage��|�.  #{�z $ Previous Page��$ RepeatButton_3������ " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � �� . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {� .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver . . ���  �@J�Opacity$ 1Q�.��(� #��z��$ 	Viewbox_5��.��F� "����@KL     �@KL ��� �    @KL 	#��z��$ Path_15���� $��Center=�$ TextBlock_5��$��Center=��� $� ���� $� ���� .F��� $ TextBlock_6������ *MasterPageNumber+ �� *TemplatedParent+�� $� ���� $
� of ���� $� ���� .F��� $ TextBlock_7������ *	PageCount+ �� *TemplatedParent+�� $� ���� $� ���� $!��NavigationCommands.NextPage��|�.  #{�z $ 	Next Page��$ RepeatButton_4������ " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � �� . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {� .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver . . ���  �@J�Opacity$ 1Q�.��(� #��z��$ 	Viewbox_6��.��F� "����        �    ��� �@KL @KL 	#��z��$ Path_16��� $��2��$��Right=�$ Grid_24��$��Center=� �� $
��Auto ��� �� $
��Auto �.+��� #��z��$��0��$%��NavigationCommands.DecreaseZoom��#��z��|�.  #��z��#{�z $ Decrease Zoom��$ RepeatButton_5��#��z��  �� $	 2.2Q�$	 2.2Q����� " ��.���  � ��$ 2,2,0,2q���  � �� �� " ��.R��� 
������ $ 	Border_41��! �� *	Padding+ �� *TemplatedParent+._�(� $" Content��$ 0.5,0.5&�$ 	Viewbox_2��.���� $��32��$ Canvas_4��$��32��.+�F� $��32��$ Path_7��$��32������ *
Background+ �� *TemplatedParent+��� .��@� $# 16,16&�$$ 16Q�$% 16Q�F� $& Sheen��$��32��$ Path_8��$��32����� .��@� $# 16,16&�$$ 16Q�$% 16Q����� $	' 0,1&�$	( 0,0&�.��� $ 	#99FFFFFF��$ 0Q�� $ 	#66000000��$ 1Q�F� $) 16��$��5��$* 16��$ Path_9������ *
Foreground+ �� *TemplatedParent+���� $
+ 10,0&�$, -10,0&� P�  �@J�IsMouseOver . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � �� . . ��� $ Content�� � z� �� $ 1Q�$ 1Q��� $ Sheen�� � �� �� $	' 0,1&�$	( 0,0&�.��� $ 	#99000000��$ 0Q�� $ 	#66FFFFFF��$ 1Q� �� " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � �� . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {� .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver . . ���  �@J�Opacity$ 1Q��� $��1��|�.  #��z��$��Stretch=�#. z- #0 z/ #2 z1 #3 z- #4 z- $ Slider_1��$��Center=�$	��100��5 �� *Zoom+$6 TwoWay=� �� *TemplatedParent+���� " ��.���  �@��IsPressAndHoldEnabled .  ��  � |� .  ��  �@�OverridesDefaultStyle . ��  � �� �� *
Background+ �� *TemplatedParent+��  � ��$ 1q���  � �� �	#00000000��  � �� �� *
Foreground+ �� *TemplatedParent+��  �@��IsSnapToTickEnabled . ��  �@J�RenderTransformOrigin$ 0.5,0.5&���  � ��$ 50����  � ��$ 0����  � ��$
 Auto����  � ��$	 2,0q���  � ��$
 Auto����  �@J�Opacity$
 0.75Q���  �@��Delay$
 1000����  �@��Interval$	 250����  � �� �� " ��.R��� 
������ #! z��$ 	Border_45��._�� }�.  $ Grid_22�� �� $��* �7 �� $
��Auto ��� $
��Auto �#��z���� $
��Auto ��� $��* �.+�m� $8 TopTick��#9 z��$��4��$	: Top=�$��0��$ 	TickBar_1��$y�	Collapsed=��� #��z��$ 2��$��2��$	; 0.5Q�$��1��$ 	Border_46���� $��4��$��1��$ Canvas_7��$��Center=�.+��� $< PART_SelectionRange��#��z��$��2��$* 1��$ Rectangle_17��$y�Hidden=�]� $= 
PART_Track��$��1��$ 
PART_Track��> �� $��Slider.DecreaseLarge��$ RepeatButton_1������ " ��.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ $ 	Border_43��? �� $��Slider.IncreaseLarge��$ RepeatButton_2������ " ��.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ $ 	Border_43��@ n� #��z��$ Thumb_5������ " n�.���  � �� �� *
Background+ �� *TemplatedParent+��  � |� .  ��  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � ��$ 6q���  � �� �� " n�.R��� 
������ $��-2q�$ 	Border_44��! �� *	Padding+ �� *TemplatedParent+._�(� $��Center=�$ 	Viewbox_4��$��Center=����� *
FontSize+ �� *TemplatedParent+  �� $ 1Q�$ 1Q�.���� $��17��$ Canvas_6��$��13��.+�F� X����@B     р��     ��ا @B ��ا @]� Ѡ.c @f� �D @f�    @]� �    @B �@B     $ Path_13������ *
Foreground+ �� *TemplatedParent+F� X����@B     р��     ��ا @B ��ا @]� Ѡ.c @f� �D @f�    @]� �    @B �@B     $A Round=�$B Round=�#��z��$
��0.75��$C 1Q�$D Round=�$ Path_14������ $' 1,0.5&�$( 0,0.5&�E V� .~�T� $
F -0.5Q�$
G -0.5Q��� $ 1Q�$ 1Q��� $H 0Q�$I 0Q��� $	J 110Q�T� $	F 0.5Q�$	G 0.5Q�.��� $ 	#CC999999��$	 0.5Q�� $ 	#CCFFFFFF��$	 0.8Q�� $ 	#CCFFFFFF��$	 0.2Q�m� $8 
BottomTick��#9 z��$��4��$: Bottom=�$��2��$ 	TickBar_2��$y�	Collapsed=� P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@J�IsMouseOver . . ���  � z� �� $
 1.05Q�$
 1.05Q�P�  �@J�IsMouseOver . . ���  �@J�Opacity$ 1Q���  � z� �� $
 1.05Q�$
 1.05Q�P�  � {� .  . ���  �@J�Opacity$	 0.3Q��� #��z��$��2��$%��NavigationCommands.IncreaseZoom��#��z��|�.  #��z��#{�zK $ Increase Zoom��$ RepeatButton_6��#��z��  �� $	 2.2Q�$	 2.2Q����� " ��.���  � ��$ 0,2,2,2q���  � �� �� " ��.R��� 
������ $ 	Border_42��! �� *	Padding+ �� *TemplatedParent+._�(� $" Content��$ 0.5,0.5&�$ 	Viewbox_3��.���� $��32��$ Canvas_5��$��32��.+�F� $��32��$ Path_10��$��32������ *
Background+ �� *TemplatedParent+��� .��@� $# 16,16&�$$ 16Q�$% 16Q�F� $& Sheen��$��32��$ Path_11��$��32����� .��@� $# 16,16&�$$ 16Q�$% 16Q����� $	' 0,1&�$	( 0,0&�.��� $ 	#99FFFFFF��$ 0Q�� $ 	#66000000��$ 1Q�F� $) 16��$��5��$* 16��$ Path_12������ *
Foreground+ �� *TemplatedParent+��� .���� $
+ 10,0&�$, -10,0&��� $
+ 0,10&�$, 0,-10&� P�  �@J�IsMouseOver . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � �� . . ��� $ Content�� � z� �� $ 1Q�$ 1Q��� $ Sheen�� � �� �� $	' 0,1&�$	( 0,0&�.��� $ 	#99000000��$ 0Q�� $ 	#66FFFFFF��$ 1Q� �� " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � �� . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {� .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver . . ���  �@J�Opacity$ 1Q��� 
�������#��z��#��z��#��z��#��z��$ 	Border_47��#��z��$ 	20,20,0,0��._��� }�. $ AdornerDecorator_1��.u�\� L .  M . $N 0��$O 1��$P Local=�$ DocumentPageView_1��Q Z�  ]� R �� +S �@.�#IsInactiveSelectionHighlightEnabledT . �� S �@.�IsSelectionActiveT .  ./���  �@.�SelectionBrush# � /��� " R�.���  �@�OverridesDefaultStyle . ��  �@J�SnapsToDevicePixels . ��  � ��$
 Auto=���  � ��$
 Auto=���  � �� �� " R�.R��� $��Border��$ 20��
����������	#00000000$��1q�._��� |�.  ��.  $��4q�.���� .��� .+�]� -   U ]�Name$U spRightUpper��$��1��$��2��$��0��.+��� $ 20��
�������$��6q�._�� .+��� $��4q�$ 20��._�R� -   V R�Name$V tvStoryTree��
���   �$��Disabled=�