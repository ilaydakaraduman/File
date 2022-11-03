using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileHW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<int> sayilar = new List<int>(); //sayiların tutulacağı yer
        Random rand = new Random();
        bool kontrol;
        bool son,ilk;
        int mod=0;
        int sonvalue;
        int ilkvalue ;
        int prop =1;
        int index=0;
        private void Form1_Load(object sender, EventArgs e)
        {
             kontrol = true;
              son = true;
              ilk = false;

        }
        public static bool asalmi(int sayi)
        {
            bool durum = false;
            //prop  değerini etkilemek için bir sayı eklenmesini istiyoruz
            //formüllere göre modu verilen sayının üstü asal sayı en iyi performans verir
            //bu sayıyı bulmak için asal sayıyı buluyoruz
            int kontrol = 0;

            for (int i = 2; i < sayi; i++)
            {
                if (sayi % i == 0)
                {
                    kontrol = 1;
                    break;
                }
            }
            if (kontrol == 1)
            {
                durum = false;
            }
            else
            {
                durum = true;
            }

            return durum;
        }

        public int Sirabulma(int key)
        {
            if (asalmi(key))
            {
                 key++;
                while (!asalmi(key))
                {
                    key++;
                }//formül için üst ilk asal sayı bulma metodu
                return key;
            }
            else
            {
                while (!asalmi(key))
                {
                    key++;
                }//formül için üst ilk asal sayı bulma metodu
                return key;
            }


        }
        public void ListOlusturma(int key)
        {
            sayilar.Clear();
            int[] sayil = new int[key-1];
            int rasgele;

            for (int i = 0; i < sayil.Length; i++)
            {
                bool dogrula = true;
                rasgele = rand.Next(1, 900);
                foreach (var x in sayil)
                {
                    if (x == rasgele)
                    {
                        i--;
                        dogrula = false;
                        break;
                    }
                }
                if (dogrula)
                {
                    sayil[i] = rasgele;
                }

            }
           

            for (int i = 0; i < sayil.Length; i++)
            {
                sayilar.Add(sayil[i]);
            }

          
        }
        public int Hesaplama(int key)
        {
            int gelenmod = key % mod;
            return gelenmod;
        }
        public void sayilariYerlestirme(int ilkvalue,int sonvalue)
        {
           
            foreach (var item in sayilar)
            {
                Console.WriteLine(item);
                int tempindex=-1;
                if (dataGridView1.Rows[Hesaplama(item)].Cells[2].Value!=null)
                {
                     tempindex = Convert.ToInt32(dataGridView1.Rows[Hesaplama(item)].Cells[2].Value);
                }
               
                prop = 1;
                if (dataGridView1.Rows[Hesaplama(item)].Cells[1].Value == null)
                {
                    dataGridView1.Rows[Hesaplama(item)].Cells[1].Value = item;
                    dataGridView2.Rows[index].Cells[0].Value = item;
                    dataGridView2.Rows[index].Cells[1].Value = prop;
                    index++;
                }
                else
                {
                    if (son==true && ilk==false)
                    { //sona ekleme 
                       
                        try
                        {
                            son = false;
                            ilk = true;
                            while (dataGridView1.Rows[sonvalue].Cells[1].Value != null)
                            {
                                sonvalue--;
                            }
                            dataGridView1.Rows[sonvalue].Cells[1].Value = item; //en alta yerleştirildi
                            if (tempindex == -1)
                            {
                                dataGridView1.Rows[Hesaplama(item)].Cells[2].Value = sonvalue;
                                dataGridView2.Rows[index].Cells[0].Value = item;
                                dataGridView2.Rows[index].Cells[1].Value = prop + 1;
                                index++;
                            }

                            else
                            {
                                prop = 2;

                                while (dataGridView1.Rows[tempindex].Cells[2].Value != null)
                                {
                                    tempindex = Convert.ToInt32(dataGridView1.Rows[tempindex].Cells[2].Value);
                                    ++prop;
                                }
                                dataGridView1.Rows[tempindex].Cells[2].Value = sonvalue;
                                dataGridView2.Rows[index].Cells[0].Value = item;
                                dataGridView2.Rows[index].Cells[1].Value = prop+1 ;
                                index++;
                            }

                        }
                        catch (Exception e)
                        {

                            MessageBox.Show(sonvalue.ToString()+ e.ToString());
                        }

                      
                    }
                    else if (son == false && ilk == true)
                    { //sıra ilk eklemeye geçtiği zaman
                       
                        try
                        {
                            ilk = false;
                            son = true;
                            while (dataGridView1.Rows[ilkvalue].Cells[1].Value != null)
                            {
                                ilkvalue++;
                            }
                            dataGridView1.Rows[ilkvalue].Cells[1].Value = item; //üste yerleşti
                            if (tempindex == -1)
                            {
                                dataGridView1.Rows[Hesaplama(item)].Cells[2].Value = ilkvalue;
                                dataGridView2.Rows[index].Cells[0].Value = item;
                                dataGridView2.Rows[index].Cells[1].Value = prop +1;
                                index++;
                            }
                            else
                            {
                                prop = 2;

                                while (dataGridView1.Rows[tempindex].Cells[2].Value != null)
                                {
                                    tempindex = Convert.ToInt32(dataGridView1.Rows[tempindex].Cells[2].Value);
                                    ++prop;
                                }
                                dataGridView1.Rows[tempindex].Cells[2].Value = ilkvalue;
                                dataGridView2.Rows[index].Cells[0].Value = item;
                                dataGridView2.Rows[index].Cells[1].Value = prop+1;
                                index++;
                            }

                        }
                        catch (Exception e)
                        {

                            MessageBox.Show(ilkvalue.ToString() + e.ToString());
                        }
                       
                    }
                }
            }
           
        }
       
        public bool BulunduMu(int value)
        {
            bool bulundu = false;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) == value)
                {
                   
                    bulundulbl.Text = "It is in table " + i.ToString() + " ..";
                    bulundu = true;
                    break;
                }
                

            }
            
            return bulundu;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(numericUpDown3.Value);
            if (BulunduMu(value))
            {
                AramaLbl.Text = "value found.."; //değeri bulursa true döndürme ve yazdırma
            }
            else
            {
               bulundulbl.Text = "";
               AramaLbl.Text = "value not found..";
            }

            
        }
        public void PackageFactor(int key)
        {
            double bosdeger = 0;
            double package = 0;
            for (int i = 0; i < key; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value != null) // boş olmayan indexler hesaplanır
                {
                    bosdeger++;
                }
            }
            package = (bosdeger / key) * 100; // dolu indexler toplam index sayısına bölünüpü 100 le çarpılır
            packfaclbl.Text = Math.Round(package, 2).ToString();
        }
        public void AverageProb(int deger)
        {
            float total = 0;
            float ort = 0;
            index = 0;  //average prop hesaplama
            for (int i = 0; i < deger; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value != null)
                {
                    total += Convert.ToSingle(dataGridView2.Rows[i].Cells[1].Value);
                    index++;

                }
            }
            lblsum.Text = total.ToString();
            ort = total / index; 
            avrgproplbl.Text = ort.ToString() ;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
          
            if (kontrol)
            {

                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear(); //her güncellemede yenilenmesi amacıyla
                dataGridView1.RowCount = Sirabulma(Convert.ToInt32(numericUpDown1.Value));
                dataGridView2.RowCount = Sirabulma(Convert.ToInt32(numericUpDown1.Value));
                ilk = false;
                son = true;
                ilkvalue = 0;
                sonvalue = dataGridView1.RowCount - 1;
                index = 0;
                mod = Convert.ToInt32(numericUpDown1.Value);
                ListOlusturma(dataGridView1.RowCount);
               
                sayilariYerlestirme(ilkvalue, sonvalue);
                Console.WriteLine(sonvalue);
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {

                    dataGridView1.Rows[i].Cells[0].Value = i;
                    
                }
                //label2.Text = Sirabulma(Convert.ToInt32(numericUpDown1.Value)).ToString();
                PackageFactor(dataGridView1.RowCount);
                AverageProb(dataGridView2.RowCount);


            }
            

        }
        //public int Hesaplamam(int sayi)
        //{
        //    int gelenmod = sayi % 11;
        //    return gelenmod;
        //}
        //public int Contiune(int key)
        //{
        //    int newkey = (key / 11) % 11;
        //    return newkey;
        //}
        //public void değerEkleme()
        //{
        //ListOlusturma(11);
        //    int TempDeger = 1;
        //    foreach (var item in sayilar)
        //    {
        //        Console.WriteLine(item);

        //        prop = 1;
        //        if (dataGridView1.Rows[Hesaplamam(item)].Cells[1].Value == null)
        //        {
        //            dataGridView1.Rows[Hesaplama(item)].Cells[1].Value = item;
        //            dataGridView2.Rows[index].Cells[0].Value = item;
        //            dataGridView2.Rows[index].Cells[1].Value = prop;
        //            index++;
        //        }
        //        else //iki seçenek var +eklenmiş halinin kontrolü 
        //        {
        //            TempDeger = Convert.ToInt32(dataGridView1.Rows[Hesaplamam(item)].Cells[1].Value) - 1;
        //            int ilerleme;
        //            while (Convert.ToInt32(dataGridView1.Rows[Hesaplamam(item)].Cells[1].Value) != -1)
        //            {
        //                ilerleme = Hesaplamam(item) + Contiune(item); //CONTİUNE
        //                if (ilerleme >= 11)
        //                {
        //                    ilerleme = ilerleme - 11;
        //                }
        //                if (dataGridView1.Rows[ilerleme].Cells[1].Value == null)
        //                {
        //                    dataGridView1.Rows[ilerleme].Cells[1].Value = item;
        //                }
        //                else
        //                {
        //                    TempDeger = Convert.ToInt32(dataGridView1.Rows[Hesaplama(item)].Cells[2].Value);
        //                    ilerleme = Contiune(Convert.ToInt32(dataGridView1.Rows[TempDeger].Cells[1].Value)) + Hesaplama(item);
        //                }

        //            }



        //            if (true)
        //            {

        //            }

        //        }
        ////    }
        //}


    }
}
