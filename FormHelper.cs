using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Server
{
    internal class FormHelper
    {
        private static Form rootForm;
        private static Dictionary<string, Form> forms;

        public void Initialize(Form root)
        {
            if (rootForm is not null)
            {
                return;
            }
            forms = new Dictionary<string, Form>();
            rootForm = root;
        }

        public T OpenForm<T>() where T : Form
        {
            var formName = typeof(T).Name;

            if (forms.ContainsKey(formName))
            {
                var alreadyOpenedForm = forms[formName];
                alreadyOpenedForm.BringToFront();
                return null;
            }

            var f = Activator.CreateInstance<T>(); // method ile instance alma yöntemi (Reflection)
            //var f = new T(); ama bunu yazamayız. Bkz. Generic konusu
            f.ShowInTaskbar = false;
            f.MaximizeBox = false;
            f.MinimizeBox = false;
            f.FormBorderStyle = FormBorderStyle.FixedSingle;
            f.FormClosing += OnChildFromClosing; // Her form oluşturulduğunda closing event'ine ship olsun
            f.Show();

            forms.Add(typeof(T).Name, f);

            return f;
        }

        private void OnChildFromClosing(object sender, FormClosingEventArgs e)
        {
            var form = (Form)sender;
            if (forms.ContainsKey(form.Name))
            {
                forms.Remove(form.Name);
                form.Dispose();
            }
        }
    }
}
