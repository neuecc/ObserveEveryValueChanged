ObserveEveryValueChanged
---
Voodoo Magic for WPF.

You can observe all properties that is not implements `INotifyPropertyChanged`.

Sample
---

```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.ObserveEveryValueChanged(x => x.Width).Subscribe(x => WidthText.Text = x.ToString());
        this.ObserveEveryValueChanged(x => x.Height).Subscribe(x => HeightText.Text = x.ToString());
    }
}
```

![wpfgif](https://cloud.githubusercontent.com/assets/46207/15827037/ed8a72c6-2c44-11e6-81d6-b90dabf0afa9.gif)

`ObserveEveryValueChanged(propertySelector)`. That's all.
