using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Modelo
{
    public partial class Form1 : Form
    {
        Stack<Pallet> pilha = new Stack<Pallet>(); //global

        public Form1()
        {
            InitializeComponent();
            carregar();
        }

        void salvar()
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open("pallets.txt", FileMode.Create)))
            {
                writer.Write(pilha.Count());
                foreach (Pallet p in pilha)
                {
                    writer.Write(p.Nome);
                    writer.Write(p.Quantidade);
                }
            }
        }

        void carregar()
        {
            if (File.Exists("pallets.txt"))
            {
                Stack<Pallet> aux = new Stack<Pallet>();
                using (BinaryReader reader = new BinaryReader(File.Open("pallets.txt", FileMode.Open)))
                {
                    int qtd = reader.ReadInt32();
                    for (int i = 0; i < qtd; i++) { 
                    Pallet p = new Pallet();
                    p.Nome = reader.ReadString();
                    p.Quantidade = reader.ReadInt32();
                    aux.Push(p);
                  }// fim for
                }
                foreach (Pallet p in aux)
                {
                    pilha.Push(p);
                }// fim foreach
                mostraPilha();
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            salvar();
            this.Close();
        }

        private void BtnSobre_Click(object sender, EventArgs e)
        {

        }

        void mostraPilha()
        {
            listPallets.Items.Clear();
            foreach (Pallet p in pilha)
            {
                listPallets.Items.Add(p.Nome + ": "+p.Quantidade);
            }
        }

        void limpar()
        {
            txtProd.Clear();
            txtQtd.Clear();
            txtProd.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Pallet p = new Pallet();
            p.Nome = txtProd.Text;
            p.Quantidade = Convert.ToInt32(txtQtd.Text);
            pilha.Push(p);
            mostraPilha();
            limpar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int quantia = Convert.ToInt32(txtQtd.Text);
            
            Stack<Pallet> aux = new Stack<Pallet>();
            while (pilha.Count > 0)
            {
                Pallet p = new Pallet();

                p = pilha.Peek(); // pegando o pallet que esta no topo
                
                if (p.Nome.Equals(txtProd.Text))
                {
                        p.Quantidade = p.Quantidade - quantia;
                        // realizar os procedimentos de remoção 
                        //testar se vai remover total ou parcial
                    if (p.Quantidade <= 0) {
                        pilha.Pop();
                        break;
                    }
                    break;
                    // realizar os procedimentos de remoção 
                    //testar se vai remover total ou parcial
                }
                else {
                    aux.Push(pilha.Pop());
                    // passar o pallet para a pilha auxiliar 
                }
            }// fim while
            
            // depois de remover tem que devolver do aux para a pilha
            foreach (Pallet p in aux) {
                pilha.Push(p);
                //retonar os valores da aux para a pilha principal
            }// fim foreach
            
            mostraPilha();
            limpar();
        }
    }
}
