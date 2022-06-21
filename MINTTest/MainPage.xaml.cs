using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MINTTest.ViewModels;
using Xamarin.Forms;

namespace MINTTest
{
    public partial class MainPage : ContentPage
    {
        MainViewModel vm;

        int CurrentTabIndex = 0;

        public MainPage()
        {
            InitializeComponent();
            vm = new MainViewModel();
            BindingContext = vm;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            foreach (var slotGroup in vm.Data.slotGroups)
            {
                RowDefinitionCollection RowCollection = new RowDefinitionCollection();
                RowCollection.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                RowCollection.Add(new RowDefinition() { Height = new GridLength(3, GridUnitType.Absolute) });

                Grid GridTab = new Grid();
                GridTab.WidthRequest = 120;
                GridTab.RowSpacing = 0;
                GridTab.HorizontalOptions = LayoutOptions.FillAndExpand;
                GridTab.RowDefinitions = RowCollection;

                BoxView boxView = new BoxView();
                boxView.Color = Color.White;
                boxView.HorizontalOptions = LayoutOptions.FillAndExpand;
                Grid.SetColumn(boxView, 0);
                Grid.SetRow(boxView, 0);

                BoxView boxView2 = new BoxView();
                boxView2.HorizontalOptions = LayoutOptions.FillAndExpand;
                boxView2.VerticalOptions = LayoutOptions.Start;
                Grid.SetColumn(boxView2, 0);
                Grid.SetRow(boxView2, 1);

                Label tabTitle = new Label();
                tabTitle.SetBinding(Label.TextProperty, "slotGroupName");
                tabTitle.BindingContext = slotGroup;
                tabTitle.FontAttributes = FontAttributes.Bold;
                tabTitle.HorizontalTextAlignment = TextAlignment.Center;
                tabTitle.VerticalTextAlignment = TextAlignment.Center;

                Grid.SetColumn(tabTitle, 0);
                Grid.SetRow(tabTitle, 0);

                if (vm.Data.slotGroups.IndexOf(slotGroup) == 0)
                {
                    boxView2.Color = Color.Black;
                    boxView2.HeightRequest = 2;
                    tabTitle.TextColor = Color.Black;
                }
                else
                {
                    boxView2.Color = Color.FromHex("#A8A7A8");
                    boxView2.HeightRequest = 1;
                    tabTitle.TextColor = Color.FromHex("#A8A7A8");
                }

                GridTab.Children.Add(boxView);
                GridTab.Children.Add(boxView2);
                GridTab.Children.Add(tabTitle);

                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += TapGestureRecognizer_TabTapped;
                GridTab.GestureRecognizers.Add(tapGestureRecognizer);

                TabBars.Children.Add(GridTab);

                ListView ResourceslistView = new ListView();
                ResourceslistView.Margin = new Thickness(10, 0);
                ResourceslistView.SelectionMode = ListViewSelectionMode.None;
                ResourceslistView.SetBinding(ListView.ItemsSourceProperty, "resources");
                ResourceslistView.BindingContext = slotGroup;
                ResourceslistView.SeparatorVisibility = SeparatorVisibility.None;
                ResourceslistView.HasUnevenRows = true;

                if (vm.Data.slotGroups.IndexOf(slotGroup) == 0)
                {
                    ResourceslistView.IsVisible = true;
                }
                else
                {
                    ResourceslistView.IsVisible = false;
                }

                ResourceslistView.ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    nameLabel.SetBinding(Label.TextProperty, "fullName");
                    nameLabel.VerticalTextAlignment = TextAlignment.Center;

                    Label userIDLabel = new Label();
                    userIDLabel.SetBinding(Label.TextProperty, "userId");
                    userIDLabel.VerticalTextAlignment = TextAlignment.Center;
                    userIDLabel.TextColor = Color.FromHex("A8A8A8");

                    Grid GridImage = new Grid();
                    GridImage.WidthRequest = 60;
                    GridImage.HeightRequest = 60;
                    GridImage.RowSpacing = 0;
                    GridImage.HorizontalOptions = LayoutOptions.FillAndExpand;

                    Frame frame = new Frame()
                    {
                        CornerRadius = 25,
                        HeightRequest = 50,
                        WidthRequest = 50,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Padding = 0,
                        IsClippedToBounds = true,
                        HasShadow = false
                    };

                    Frame frameBorder = new Frame()
                    {
                        CornerRadius = 28,
                        HeightRequest = 56,
                        WidthRequest = 56,
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        Padding = 3,
                        HasShadow = false,
                        BorderColor = Color.FromHex("A8A8A8")
                    };

                    frameBorder.Content = frame;

                    Image image = new Image();
                    image.SetBinding(Image.SourceProperty, "photo");
                    image.HorizontalOptions = LayoutOptions.Center;
                    image.VerticalOptions = LayoutOptions.Center;
                    image.HeightRequest = 60;
                    image.WidthRequest = 60;
                    image.Aspect = Aspect.AspectFit;
                    frame.Content = image;

                    Grid.SetColumn(frameBorder, 0);
                    Grid.SetRow(frameBorder, 0);

                    GridImage.Children.Add(frameBorder);
                    GridImage.Margin = new Thickness(5);

                    Grid GridContainer = new Grid();
                    ColumnDefinitionCollection ColumnCollection = new ColumnDefinitionCollection();
                    RowDefinitionCollection RowCollection2 = new RowDefinitionCollection();
                    ColumnCollection.Add(new ColumnDefinition() { Width = new GridLength(70, GridUnitType.Absolute) });
                    RowCollection2.Add(new RowDefinition() { Height = new GridLength(70, GridUnitType.Absolute) });
                    RowCollection2.Add(new RowDefinition() { Height = GridLength.Auto });
                    ColumnCollection.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
                    ColumnCollection.Add(new ColumnDefinition() { Width = new GridLength(50, GridUnitType.Absolute) });
                    GridContainer.ColumnDefinitions = ColumnCollection;
                    GridContainer.RowDefinitions = RowCollection2;

                    Grid.SetColumn(GridImage, 0);
                    Grid.SetRow(GridImage, 0);

                    StackLayout labelStack = new StackLayout();
                    labelStack.VerticalOptions = LayoutOptions.Center;
                    labelStack.Children.Add(nameLabel);
                    labelStack.Children.Add(userIDLabel);

                    Grid.SetColumn(labelStack, 1);
                    Grid.SetRow(labelStack, 0);

                    GridContainer.Children.Add(GridImage);
                    GridContainer.Children.Add(labelStack);

                    Button arrow = new Button();
                    arrow.ImageSource = "Backarrow.png";
                    arrow.HeightRequest = 20;
                    arrow.WidthRequest = 20;
                    arrow.BackgroundColor = Color.White;
                    arrow.Opacity = 0.5;
                    arrow.HorizontalOptions = LayoutOptions.Center;
                    arrow.VerticalOptions = LayoutOptions.Center;
                    arrow.SetBinding(Button.CommandParameterProperty, ".");

                    arrow.Clicked += TapGestureRecognizer_ArrowTapped;

                    Grid.SetColumn(arrow, 2);
                    Grid.SetRow(arrow, 0);

                    GridContainer.Children.Add(arrow);

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = GridContainer
                    };
                });

                Grid.SetColumn(ResourceslistView, 0);
                Grid.SetRow(ResourceslistView, 2);

                MainGrid.Children.Add(ResourceslistView);
            }
        }

        private void TapGestureRecognizer_ArrowTapped(object sender, EventArgs e)
        {
            var button = (Button)sender;
            if (button.Rotation == 0)
            {
                button.RotateTo(180);
            }
            else
            {
                button.RotateTo(0);
                if (((Grid)button.Parent).Children.Count == 4)
                {
                    ((Grid)button.Parent).Children.RemoveAt(3);
                    return;
                }
            }

            if (((Resource)button.CommandParameter).certificates.Count == 0)
            {
                return;
            }

            Grid containerGrid = ((Grid)button.Parent);
            StackLayout CertificatesList = new StackLayout();
            CertificatesList.Margin = new Thickness(10, 0);
            Grid Header = new Grid();
            RowDefinitionCollection RowCollection = new RowDefinitionCollection();
            RowCollection.Add(new RowDefinition() { Height = new GridLength(35, GridUnitType.Absolute) });
            Header.RowDefinitions = RowCollection;

            BoxView BackgroundHeader = new BoxView();
            BackgroundHeader.BackgroundColor = Color.FromHex("#0060C0");

            Label HeaderText = new Label() { Margin = new Thickness(10), Text = "Certificates", FontAttributes = FontAttributes.Bold, TextColor = Color.White, VerticalTextAlignment = TextAlignment.Center };

            Header.Children.Add(BackgroundHeader);
            Header.Children.Add(HeaderText);

            Header.HorizontalOptions = LayoutOptions.FillAndExpand;
            Header.VerticalOptions = LayoutOptions.FillAndExpand;

            CertificatesList.Children.Add(Header);

            foreach (var item in ((Resource)button.CommandParameter).certificates)
            {
                if (((Resource)button.CommandParameter).certificates.IndexOf(item) == ((Resource)button.CommandParameter).certificates.Count - 1)
                {
                    CertificatesList.Children.Add(new Label() { Text = item, Margin = new Thickness(3) });
                }
                else
                {
                    CertificatesList.Children.Add(new Label() { Text = item, Margin = new Thickness(3) });
                    CertificatesList.Children.Add(new BoxView() { HeightRequest = 1, BackgroundColor = Color.LightGray, HorizontalOptions = LayoutOptions.FillAndExpand });
                }

            }

            Grid.SetColumn(CertificatesList, 0);
            Grid.SetRow(CertificatesList, 1);
            Grid.SetColumnSpan(CertificatesList, 3);

            Device.BeginInvokeOnMainThread(() =>
            {
                containerGrid.Children.Add(CertificatesList);
            });

        }

        void TapGestureRecognizer_TabTapped(System.Object sender, System.EventArgs e)
        {
            foreach (var item in ((StackLayout)((Grid)sender).Parent).Children)
            {
                Grid container = (Grid)item;

                ((BoxView)container.Children[1]).Color = Color.FromHex("#A8A7A8");
                ((BoxView)container.Children[1]).HeightRequest = 1;
                ((Label)container.Children[2]).TextColor = Color.FromHex("#A8A7A8");

            }

            Grid tab = (Grid)sender;
            ((BoxView)tab.Children[1]).Color = Color.Black;
            ((BoxView)tab.Children[1]).HeightRequest = 2;
            ((Label)tab.Children[2]).TextColor = Color.Black;

            for (int i = 2; i < MainGrid.Children.Count; i++)
            {
                MainGrid.Children[i].IsVisible = false;
            }

            int TabIndex = ((StackLayout)((Grid)sender).Parent).Children.IndexOf(tab);

            var list = MainGrid.Children[TabIndex + 2];
            list.IsVisible = true;

            if (TabIndex > CurrentTabIndex)
            {
                list.TranslationX = 500;
                list.TranslateTo(0, list.TranslationY);
            }
            else
            {
                list.TranslationX = -500;
                list.TranslateTo(0, list.TranslationY);
            }

            CurrentTabIndex = TabIndex;
        }

        void SearchBar_TextChanged(System.Object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(((SearchBar)sender).Text))
            {
                ((SearchBar)sender).ScaleXTo(1);
            }
            else
            {
                ((SearchBar)sender).ScaleXTo(0.95);
            }
        }
    }
}
