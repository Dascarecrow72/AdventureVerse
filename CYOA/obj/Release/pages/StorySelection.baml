   M S B A M L   `   `   `  ���� U  QPresentationUI, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35[>clr-namespace:System.Windows.Documents;assembly=PresentationUISystem.Windows.Documents   CYOA   CYOA.StorySelection   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     T	Documents>clr-namespace:System.Windows.Documents;assembly=PresentationUI       N� Title$  AdventureVerse��$	��700��$
��1050�� � 	Resources    brdChoiceStyle �� 
TargetType �� Property �� Value � Color � Offset �� Triggers P� Property	 P� Value + 'FlowDocumentPageViewerStyleRoundCorners
 �� 
ResourceId �� TypeInTargetAssembly;   5System.Windows.Documents.PresentationUIStyleResources-  'System.Windows.Input.KeyboardNavigation �� 
TargetType � Language
 J� Uid � ColumnDefinitions ��Name �� CornerRadius �� Orientation Y� CanGoToPreviousPage � ToolTip �� BasedOn ��Name �� ContentStringFormat �� ContentStringFormat J� RenderTransformOrigin �� Triggers �� 
TargetName �� ScaleY �� ScaleX �� RelativeSource Y� CanGoToNextPage  .� CanDecreaseZoom! � LayoutTransform" �� Padding# (�Name$ @� Center% @� RadiusY& @� RadiusX' F�Name( �� EndPoint) �� 
StartPoint* �� Left
+ �� Top, �� EndPoint- �� 
StartPoint. .� ZoomIncrement/ � LargeChange0 .� MaxZoom1 � Maximum2 .� MinZoom3 � Minimum4 � SmallChange5 �� TickFrequency6 � Value7 �� Mode8 � RowDefinitions9 m�Name: m� Fill; m� 	Placement< J� Opacity= ��Name> ]�Name? ]� DecreaseRepeatButton@ ]� IncreaseRepeatButtonA ]� ThumbB �� StrokeEndLineCapC �� StrokeDashCapD �� StrokeMiterLimitE �� StrokeLineJoinF �� RelativeTransformG T� XH T� YI �� AngleYJ �� AngleXK �� AngleL .� CanIncreaseZoomM   	IsTabStopN Y� IsMasterPageO \� 
PageNumberP   TabIndexQ   TabNavigationR \� DocumentPaginatorS ]� 
ConditionsT �� PropertyU �� Value��%>*  &	        &	      �� " ��.���  � �� �� ��� $ White��$ 0Q� P�  �@J�IsMouseOver	 . . ���  � ��
 ������� " .�.���  � ��
 ���� ��  �@�ContextMenu C� *�� $ 
 PUIFlowViewers_ContextMenu��"  +��  � ��$ Center=���  � ��$ Center=���  �@ TabNavigation$ Local=���  � �� �� " .�.R�]� $ en-us���. $ DockPanel_2��.+�� 
������ $��Bottom=�$��Stretch=�$ Grid_23�� �� $��* ��� $
��Auto ��� $��* �.+��� $ PART_FindToolBarHost��$��3��$ 	Border_60��
�������$ 	0,0,20,20���� $��1��$��Center=�$ 
Horizontal=�$ StackPanel_1��$��Center=�.+��� $%��NavigationCommands.PreviousPage��|�.  #{�z $ Previous Page��$ RepeatButton_3������ " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver	 . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � ��	 . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {�	 .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver	 . . ���  �@J�Opacity$ 1Q�.��(� #��z��$ 	Viewbox_5��.��F� "����@KL     �@KL ��� �    @KL 	#��z��$ Path_15���� $��Center=�$ TextBlock_5��$��Center=��� $� ���� $� ���� .F��� $ TextBlock_6������ *MasterPageNumber+ �� *TemplatedParent+�� $� ���� $
� of ���� $� ���� .F��� $ TextBlock_7������ *	PageCount+ �� *TemplatedParent+�� $� ���� $� ���� $!��NavigationCommands.NextPage��|�.  #{�z $ 	Next Page��$ RepeatButton_4������ " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver	 . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � ��	 . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {�	 .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver	 . . ���  �@J�Opacity$ 1Q�.��(� #��z��$ 	Viewbox_6��.��F� "����        �    ��� �@KL @KL 	#��z��$ Path_16��� $��2��$��Right=�$ Grid_24��$��Center=� �� $
��Auto ��� �� $
��Auto �.+��� #��z��$��0��$%��NavigationCommands.DecreaseZoom��#��z��|�.  #��z��#{�z  $ Decrease Zoom��$ RepeatButton_5��#��z��! �� $	 2.2Q�$	 2.2Q����� " ��.���  � ��$ 2,2,0,2q���  � �� �� " ��.R��� 
������ $ 	Border_41��" �� *	Padding+ �� *TemplatedParent+._�(� $# Content��$ 0.5,0.5&�$ 	Viewbox_2��.���� $��32��$ Canvas_4��$��32��.+�F� $��32��$ Path_7��$��32������ *
Background+ �� *TemplatedParent+��� .��@� $$ 16,16&�$% 16Q�$& 16Q�F� $' Sheen��$��32��$ Path_8��$��32����� .��@� $$ 16,16&�$% 16Q�$& 16Q����� $	( 0,1&�$	) 0,0&�.��� $ 	#99FFFFFF��$ 0Q�� $ 	#66000000��$ 1Q�F� $* 16��$��5��$+ 16��$ Path_9������ *
Foreground+ �� *TemplatedParent+���� $
, 10,0&�$- -10,0&� P�  �@J�IsMouseOver	 . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � ��	 . . ��� $ Content�� � z� �� $ 1Q�$ 1Q��� $ Sheen�� � �� �� $	( 0,1&�$	) 0,0&�.��� $ 	#99000000��$ 0Q�� $ 	#66FFFFFF��$ 1Q� �� " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver	 . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � ��	 . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {�	 .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver	 . . ���  �@J�Opacity$ 1Q��� $��1��|�.  #��z��$��Stretch=�#/ z. #1 z0 #3 z2 #4 z. #5 z. $ Slider_1��$��Center=�$	��100��6 �� *Zoom+$7 TwoWay=� �� *TemplatedParent+���� " ��.���  �@��IsPressAndHoldEnabled .  ��  � |� .  ��  �@�OverridesDefaultStyle . ��  � �� �� *
Background+ �� *TemplatedParent+��  � ��$ 1q���  � �� �	#00000000��  � �� �� *
Foreground+ �� *TemplatedParent+��  �@��IsSnapToTickEnabled . ��  �@J�RenderTransformOrigin$ 0.5,0.5&���  � ��$ 50����  � ��$ 0����  � ��$
 Auto����  � ��$	 2,0q���  � ��$
 Auto����  �@J�Opacity$
 0.75Q���  �@��Delay$
 1000����  �@��Interval$	 250����  � �� �� " ��.R��� 
������ #" z��$ 	Border_45��._�� }�.  $ Grid_22�� �� $��* �8 �� $
��Auto ��� $
��Auto �#��z���� $
��Auto ��� $��* �.+�m� $9 TopTick��#: z��$��4��$	; Top=�$��0��$ 	TickBar_1��$y�	Collapsed=��� #��z��$ 2��$��2��$	< 0.5Q�$��1��$ 	Border_46���� $��4��$��1��$ Canvas_7��$��Center=�.+��� $= PART_SelectionRange��#��z��$��2��$+ 1��$ Rectangle_17��$y�Hidden=�]� $> 
PART_Track��$��1��$ 
PART_Track��? �� $��Slider.DecreaseLarge��$ RepeatButton_1������ " ��.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ $ 	Border_43��@ �� $��Slider.IncreaseLarge��$ RepeatButton_2������ " ��.���  �@�OverridesDefaultStyle . ��  � �� .  ��  � |� .  ��  � �� �� " ��.R��� 
������ $ 	Border_43��A n� #��z��$ Thumb_5������ " n�.���  � �� �� *
Background+ �� *TemplatedParent+��  � |� .  ��  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � ��$ 6q���  � �� �� " n�.R��� 
������ $��-2q�$ 	Border_44��" �� *	Padding+ �� *TemplatedParent+._�(� $��Center=�$ 	Viewbox_4��$��Center=����� *
FontSize+ �� *TemplatedParent+! �� $ 1Q�$ 1Q�.���� $��17��$ Canvas_6��$��13��.+�F� X����@B     р��     ��ا @B ��ا @]� Ѡ.c @f� �D @f�    @]� �    @B �@B     $ Path_13������ *
Foreground+ �� *TemplatedParent+F� X����@B     р��     ��ا @B ��ا @]� Ѡ.c @f� �D @f�    @]� �    @B �@B     $B Round=�$C Round=�#��z��$
��0.75��$D 1Q�$E Round=�$ Path_14������ $( 1,0.5&�$) 0,0.5&�F V� .~�T� $
G -0.5Q�$
H -0.5Q��� $ 1Q�$ 1Q��� $I 0Q�$J 0Q��� $	K 110Q�T� $	G 0.5Q�$	H 0.5Q�.��� $ 	#CC999999��$	 0.5Q�� $ 	#CCFFFFFF��$	 0.8Q�� $ 	#CCFFFFFF��$	 0.2Q�m� $9 
BottomTick��#: z��$��4��$; Bottom=�$��2��$ 	TickBar_2��$y�	Collapsed=� P�  �@��TickPlacement$	 TopLeft=�. ��� $ TopTick�� � y�$ Visible=�P�  �@��TickPlacement$	 BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=�P�  �@��TickPlacement$
	 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  �@��IsSelectionRangeEnabled	 . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@J�IsMouseOver	 . . ���  � z� �� $
 1.05Q�$
 1.05Q�P�  �@J�IsMouseOver	 . . ���  �@J�Opacity$ 1Q���  � z� �� $
 1.05Q�$
 1.05Q�P�  � {�	 .  . ���  �@J�Opacity$	 0.3Q��� #��z��$��2��$%��NavigationCommands.IncreaseZoom��#��z��|�.  #��z��#{�zL $ Increase Zoom��$ RepeatButton_6��#��z��! �� $	 2.2Q�$	 2.2Q����� " ��.���  � ��$ 0,2,2,2q���  � �� �� " ��.R��� 
������ $ 	Border_42��" �� *	Padding+ �� *TemplatedParent+._�(� $# Content��$ 0.5,0.5&�$ 	Viewbox_3��.���� $��32��$ Canvas_5��$��32��.+�F� $��32��$ Path_10��$��32������ *
Background+ �� *TemplatedParent+��� .��@� $$ 16,16&�$% 16Q�$& 16Q�F� $' Sheen��$��32��$ Path_11��$��32����� .��@� $$ 16,16&�$% 16Q�$& 16Q����� $	( 0,1&�$	) 0,0&�.��� $ 	#99FFFFFF��$ 0Q�� $ 	#66000000��$ 1Q�F� $* 16��$��5��$+ 16��$ Path_12������ *
Foreground+ �� *TemplatedParent+��� .���� $
, 10,0&�$- -10,0&��� $
, 0,10&�$- 0,-10&� P�  �@J�IsMouseOver	 . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � ��	 . . ��� $ Content�� � z� �� $ 1Q�$ 1Q��� $ Sheen�� � �� �� $	( 0,1&�$	) 0,0&�.��� $ 	#99000000��$ 0Q�� $ 	#66FFFFFF��$ 1Q� �� " ��.���  � ��$ 5q���  �@��Delay$
 1000����  �@��Interval$	 250�� �� " ��.���  � |� .  ��  �@J�Opacity$	 0.5Q���  �@�Cursor$
 Hand����  � ��
 ���� ��  � ��$	 3,1q���  � �� Z� ��  � ��$ 0q���  � ��$ 0����  � ��$ 0����  � �� �� " ��.R��� #��z��$ 	Border_39��._��� $ Content��#��z��#��z��# z $ 0.5,0.5&�$ ContentPresenter_2�� P�  �@J�IsMouseOver	 . . ��� $ Content�� � z� �� $	 1.1Q�$	 1.1Q�P�  � ��	 . . ��� $ Content�� � z� �� $	 0.9Q�$	 0.9Q� P�  � {�	 .  . ���  �@J�Opacity$	 0.3Q�P�  �@J�IsMouseOver	 . . ���  �@J�Opacity$ 1Q��� 
�������#��z��#��z��#��z��#��z��$ 	Border_47��#��z��$ 	20,20,0,0��._��� }�. $ AdornerDecorator_1��.u�\� M .  N . $O 0��$P 1��$Q Local=�$ DocumentPageView_1��R Z�  ]� S �� +T �@.�#IsInactiveSelectionHighlightEnabledU . �� T �@.�IsSelectionActiveU .  ./���  �@.�SelectionBrush# � /�.��� 
�����w� �� $
��.65* ��� $
��.35* �8 �� $	��.7* ��� $	��.3* �.+�]� -   V ]�Name$V spUpperLeft��$��0��$��0��.+��� $ 20��
�������$��6q�._�� .+��� $��4q�$ 20��$��0��
�������._��� -   W ��Name$W lvAvailableAdventures��$��20q�$��24(�]� -   $V spUpperRight��$��1��$��0��.+��� $ 20��
�������$��6q�._�� .+��� $��4q�$ 20��$��0��$��3��
�������._��  �� $
��.25* ��� $	��.1* ��� $
��.65* �8 �� $	��.1* ��� $	��.1* ��� $	��.1* ��� $	��.1* ��� $	��.7* �.+��� $��0��$��Right=�$��Center=�$��SemiBold"�$��Title:���� $��1��$��Right=�$��Center=�$��SemiBold"�$��Author:���� $��2��$��Right=�$��Center=�$��SemiBold"�$��Publish Date:���� $��3��$��Right=�$��Center=�$��SemiBold"�$��Summary:���� -   X ��Name$X txtTitle��$��0��$��2��$
��Left=�$��Center=�$��SemiBold"��� -   $X 	txtAuthor��$��1��$��2��$
��Left=�$��Center=�$��SemiBold"��� -   $X txtPublishDate��$��2��$��2��$
��Left=�$��Center=�$��SemiBold"��� -   $X 
txtSummary��$��20,10,20,10q�$��4��$��3��$
��Left=�$	��Top=�$��SemiBold"�$
��Wrap=�]� -   $V spLower��$��2��$��1��.+��� $ 20��
�������$��6q�._�� .+��� $��2q�$ 20��._�]� .+�� 8 �� $	��.2* ��� $	��.2* �.+��� -	   $ brdChoiceZero��$��4q�$ 20��$��0��#��[  ._��� $��50(�$��Arial Narrow)�$
��Wrap=�$��Center=�$��Center=�$��0��$��	10,0,10,0q�$
��Bold"�$��SELECT ADVENTURE���� -
   $ brdChoiceOne��$��4,0,4,4q�$ 20��$��1��#��[  ._��� $��50(�$��Arial Narrow)�$
��Wrap=�$��Center=�$��Center=�$��0��$��	10,0,10,0q�$
��Bold"�$��DOWNLOAD NEW ADVENTURES��