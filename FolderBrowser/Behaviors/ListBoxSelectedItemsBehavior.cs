namespace FolderBrowser.Behaviors
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;
    using Models;

    public class ListBoxSelectedItemsBehavior : Behavior<ListBox>
    {
        public static readonly DependencyProperty SelectedItemsListProperty =
            DependencyProperty.Register("SelectedItemsList", typeof (List<FileDescription>),
                typeof (ListBoxSelectedItemsBehavior));

        public List<FileDescription> SelectedItemsList
        {
            get { return (List<FileDescription>)this.GetValue(SelectedItemsListProperty); }
            set { this.SetValue(SelectedItemsListProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            this.AssociatedObject.SelectionChanged += this.AssociatedObject_SelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            this.AssociatedObject.SelectionChanged -= this.AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedItemsList = this.AssociatedObject.SelectedItems.Cast<FileDescription>().ToList();
        }
    }
}