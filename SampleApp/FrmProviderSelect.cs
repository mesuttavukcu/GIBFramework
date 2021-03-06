﻿using GIBInterface;
using System;
using System.Text;
using System.Windows.Forms;

namespace SampleApp
{
    public partial class FrmProviderSelect : Form
    {
        public GIBFramework.EFatura EFatura { get; set; }

        public GIBInterface.IGIBData dataLayer { get; set; } 
        


        private IEFatura AktiveProvider { get; set; }

        public FrmProviderSelect()
        {
            InitializeComponent();
        }

        private void FrmProviderSelect_Shown(object sender, EventArgs e)
        {
            cbProviders.Items.Add("Sahte Entegrator");
            cbProviders.Items.Add("Uyumsoft");
            cbProviders.Items.Add("Uyumsoft Dot Net Standart");

            cbProviders.Items.Add("Veriban");
            cbProviders.Items.Add("Logo");

        }

        private void cbProviders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProviders.SelectedIndex >= 0)
            {
                if (cbProviders.SelectedItem != null)
                {
                    if (cbProviders.SelectedItem.ToString() == "Sahte Entegrator")
                    {
                        AktiveProvider = new SahteEntegrator.EFatura();
                    }

                    if (cbProviders.SelectedItem.ToString() == "Uyumsoft")
                    {
                        AktiveProvider = new Uyumsoft.EFatura();
                    }

                    if (cbProviders.SelectedItem.ToString() == "Uyumsoft Dot Net Standart")
                    {
                        AktiveProvider = new UyumsoftDotNetStandart.EFatura();
                    }

                    if (cbProviders.SelectedItem.ToString() == "Veriban")
                    {
                        AktiveProvider = new GIBProviders.Veriban.EFatura();
                    }
                    if (cbProviders.SelectedItem.ToString() == "Logo")
                    {
                        AktiveProvider = new GIBProviders.Logo.EFatura();
                    }

     
                    EFatura = new GIBFramework.EFatura(AktiveProvider, dataLayer);


                    if (System.IO.File.Exists(GetSettingsFileName()))
                    {
                        tbSettings.Text = System.IO.File.ReadAllText(GetSettingsFileName());
                    }
                    else
                    {
                        tbSettings.Text = EFatura.SettingsJson;
                    }
                }
            }
        }

        private void btnTamam_Click(object sender, EventArgs e)
        {
            if (cbProviders.SelectedItem != null)
            {
                EFatura.SettingsJson = tbSettings.Text;
                System.IO.File.WriteAllText(GetSettingsFileName(), tbSettings.Text, UTF8Encoding.UTF8);
                DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("Lütfen Entegratör seçiniz");
            }
        }

        private string GetSettingsFileName()
        {
            return "Settings_" + cbProviders.Text + ".json";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tbSettings.Text = EFatura.DefaultSettingsJson();
        }
    }
}
