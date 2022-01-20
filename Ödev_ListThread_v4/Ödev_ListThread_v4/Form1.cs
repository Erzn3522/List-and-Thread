using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ödev_ListThread_v4
{
    public partial class Form1 : Form
    {
        int Index = 0, counter =0;
        int add = 777;
        List<int> arrayList = new List<int>();
        List<int> arrayList2 = new List<int>();
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;    // bunun yerine invoke dene
            InitializeComponent();
        }
        private void basicTextWrite(List<int> list, int Index, System.Diagnostics.Stopwatch watch)
        {
            for (Index = 0; Index < list.Count; Index++)
            {
                counter++;
                textBox2.Text += list[Index].ToString() + "\r\n";
                
                if (Index == list.Count-1)
                {
                    watch.Stop();                    
                    label5.Text = "Çalışma Süresi: " + watch.ElapsedMilliseconds + "ms";
                    label4.Text = counter.ToString() + "/" + arrayList2.Count.ToString();
                }
            }
        }        
        private void createList(List<int> list, int length)
        {
            for (int i = 0; i < length; i++)
            {
                list.Add(i);
            }
        }
        public void writeFunc(List<int> arrayList, int Index)
        {
            lock (arrayList);
            for (Index = 0; Index < arrayList.Count; Index++)
            {
                
                if (Index % 10 == 0 && Index != 0)
                {
                    arrayList.Insert((Index), add);
                    arrayList.RemoveAt(Index + 1);
                }
                textBox1.Text += Index + ". İndis: " + arrayList[Index] + "\r\n";
                
            }
        }
        private void deleteFunc(List<int> array, int Index)
        {
            lock (arrayList);
            for (Index = 0; Index < arrayList.Count; Index++)
            {
                if (Index % 5 == 0 && Index != 0)
                {
                    textBox3.Text += Index + ". İndis: " + array[Index].ToString() + "silindi" + "\r\n";
                    arrayList.RemoveAt(Index);
                }
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            createList(arrayList, 30);
            createList(arrayList2, 30);
            Thread th1 = new Thread(() => writeFunc(arrayList, Index));
            Thread th2 = new Thread(() => deleteFunc(arrayList, Index));
            Thread th3 = new Thread(() => basicTextWrite(arrayList2, Index,watch));
            
            th1.Start();
            th2.Start();
            th3.Start();

            //textBox2.Text += arrayList2[Index].ToString() + "\r\n";            
            
            
            
        }
    }
}
