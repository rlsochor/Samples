using System;
using System.ServiceModel;
using System.Windows.Forms;
using DataWriter.SharedTypes;


namespace DataWriterTestDriver
{
    public partial class Form1 : Form
    {
        private DataWriterSoapSvc_XML.IDataWriterXMLSoapSvc XMLSvc = null;
        private DataWriterSoapSvc_JSON.IDataWriterJSONSoapSvc JSONSvc = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    XMLSvc = new DataWriterSoapSvc_XML.DataWriterXMLSoapSvcClient(new BasicHttpBinding(),
                                new EndpointAddress(textBox1.Text + "/XML"));
                    if (XMLSvc.Ping())
                    {
                        radioButton1.Text = "XML Service is active at the listed address:";
                    }
                    else
                    {
                        radioButton1.Text = "XML Service is inactive or can't be found";
                    }
                }
                catch (Exception)
                {
                    radioButton1.Text = "XML Service is not active/available";                                        
                }

            }
            else
            {
                try
                {
                    JSONSvc = new DataWriterSoapSvc_JSON.DataWriterJSONSoapSvcClient(new BasicHttpBinding(),
                                 new EndpointAddress(textBox1.Text + "/JSON"));
                    if (JSONSvc.Ping())
                    {
                        radioButton2.Text = "JSON Service is active at the listed address:";
                    }
                    else
                    {
                        radioButton2.Text = "JSON Service is either inactive or can't be found";
                    }
                }
                catch (Exception)
                {
                    radioButton2.Text = "JSON Service is not active/available";
                }

              
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SampleClass jd1 = new SampleClass()
            {
                CounterId = "MM23",
                CounterValue = 345,
                CounterTime = DateTime.Now,
                CounterReset = false
            };
            DataWriterSoapSvc_JSON.JSONData jsonData = new DataWriterSoapSvc_JSON.JSONData();
            jsonData.TimeSent = DateTime.Now;
            textBox2.Text = JSONHelper.Serialize(typeof (SampleClass), jd1);
            jsonData.Data = textBox2.Text;
            JSONSvc.WriteData(jsonData);
        }
    }
}
