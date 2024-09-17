using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlanificadorDeProcesos_Grupo_5
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
            UpdateProcessList();
            dataGridView2.SelectionChanged += new EventHandler(dataGridView2_SelectionChanged);
        }

        private void UpdateProcessList(){

            dataGridView2.Rows.Clear();
            foreach (Process p in Process.GetProcesses())
            {
                int n = dataGridView2.Rows.Add();
                dataGridView2.Rows[n].Cells[0].Value = p.ProcessName;
                dataGridView2.Rows[n].Cells[1].Value = p.Id;
                dataGridView2.Rows[n].Cells[2].Value = p.WorkingSet64;
                dataGridView2.Rows[n].Cells[3].Value = p.VirtualMemorySize64;
                dataGridView2.Rows[n].Cells[4].Value = p.SessionId;
            }
            txtContador.Text = "Procesos actuales: " + dataGridView2.Rows.Count.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProceso.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow != null && dataGridView2.CurrentRow.Cells[0].Value != null)
            {
                txtProceso.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            }
        }


        private void btnActualizar_Click(object sender, EventArgs e)
        {
            UpdateProcessList();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnDetener_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Process p in Process.GetProcesses())
                {
                    if(p.ProcessName == txtProceso.Text)
                    {
                        p.Kill();
                    }
                }
            }catch(Exception x)
            {
                MessageBox.Show("No se selecciono ningún proceso" + x, "Error al eliminar", MessageBoxButtons.OK);
            }
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
