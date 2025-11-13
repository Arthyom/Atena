using System;
using System.Linq;
using Atena.Models;

using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


namespace Atena;

public class SecondWindow : Window
{

    private readonly AtenaGlobalConfigs _configs;

    [UI] private Entry _txt_cn;
    [UI] private Button _btn_ok;

    public SecondWindow(AtenaGlobalConfigs configs) : this(new Gtk.Builder("SecondWindow.glade"))
    {
        _configs = configs;
    }


    private SecondWindow(Builder builder) : base(builder.GetObject("SecondWindow").Handle)
    {
        builder.Autoconnect(this);
        DeleteEvent += Window_DeleteEvent;
        _btn_ok.Clicked += Window_Ok_Clicked;
    }

    private void Window_Ok_Clicked(object sender, EventArgs e)
    {
        _configs.ConnectionString = _txt_cn.Text;
        if( !string.IsNullOrEmpty(_configs.ConnectionString) )
        // Window_DeleteEvent(sender,(DeleteEventArgs)e);
            Close();
    }

    private void Window_DeleteEvent(object sender, DeleteEventArgs a)
    {
        if (string.IsNullOrEmpty(_txt_cn.Text))
        {
            Application.Quit();
            a.RetVal = false;
        }
        else
            a.RetVal = true;
    }




}
