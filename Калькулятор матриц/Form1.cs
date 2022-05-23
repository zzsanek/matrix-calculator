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

namespace Калькулятор_матриц
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        int Det(int[][] Matrix) //Нахождение определителя
        {
            int Returning;
            if (Matrix.Length == 2)
            {
                Returning = Matrix[0][0] * Matrix[1][1] - Matrix[0][1] * Matrix[1][0];
            }
            else
            {
                int[][] Minor = new int[Matrix.Length - 1][]; 
                int i, j, k;
                short Minus = 1;
                int Temp;
                Returning = 0;

                for (i = 0; i < Matrix.Length; i++)
                {
                    for (j = 1; j < Matrix.Length; j++) 
                    {
                        Minor[j - 1] = new int[Matrix.Length - 1];
                        for (k = 0; k < i; k++) 
                            Minor[j - 1][k] = Matrix[j][k];

                        for (k++; k < Matrix.Length; k++)  
                            Minor[j - 1][k - 1] = Matrix[j][k];
                    }

                    Temp = Det(Minor);
                    Temp = Matrix[0][i] * Minus * Temp;
                    Returning += Temp;

                    if (Minus > 0) 
                        Minus = -1;
                    else
                        Minus = 1;
                }
            }
            return Returning;
        }

        private int MinValue(int a, int b) //нахождение миниума
        {
            if (a >= b)
                return b;
            else
                return a;
        }


        public int Rank(int[,] matrix,int n) //Нахождение ранга матрицы
        {
                
                int rang = 0;
                int q = 1;

                while (q <= MinValue(matrix.GetLength(0), matrix.GetLength(1)))
                {
                    int[,] matbv = new int[q, q];
                    for (int i = 0; i < (matrix.GetLength(0) - (q - 1)); i++)
                    {
                        for (int j = 0; j < (matrix.GetLength(1) - (q - 1)); j++)
                        {
                            for (int k = 0; k < q; k++)
                            {
                                for (int c = 0; c < q; c++)
                                {
                                    matbv[k, c] = matrix[i + k, j + c];
                                }
                            rang = q;
                            }
                        }
                    }
                    q++;
                }

                int score = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (matrix[i, j] == 0) score++;
                    }
                }
                if (score == n*n) rang = 0;
                return rang;
        }

        public double[,] Powmatrix(double colvo)  //Степень
        {

            int n = dataGridView1.RowCount;
            int m = dataGridView1.ColumnCount;
            double x = colvo;
            dataGridView3.RowCount = n;
            dataGridView3.ColumnCount = m;
            double[,] a = new double[n, m];
            double[,] c = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    a[i, j] = Convert.ToDouble(dataGridView1[i, j].Value);

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        c[i, j] = (double)Math.Pow(a[i, j],x);
                    }


            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    dataGridView3[i, j].Value = c[i, j];
                }
            return c;
        }


        //---------------------------------------------------------------------------------------------------------------------------------------------------------------
        private void button1_Click(object sender, EventArgs e) //ввод 1 матрицы
        {
            try
            {
                int n;
                int.TryParse(textBox1.Text, out n);
                dataGridView1.RowCount = n;
                dataGridView1.ColumnCount = n;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        dataGridView1.Columns[j].Width = 70;
                    }
                }
            } catch {
                MessageBox.Show("Введите размерность матрицы", "Ошибка ввода");
            } 
        }

        private void button2_Click(object sender, EventArgs e)//ввод 2 матрицы
        {
            try
            {
                int n;
                int.TryParse(textBox2.Text, out n);
                dataGridView2.RowCount = n;
                dataGridView2.ColumnCount = n;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        dataGridView2.Columns[j].Width = 70;
                    }
                }
            }catch{
                MessageBox.Show("Введите размерность матрицы","Ошибка ввода");
            }
        }


        private void button3_Click(object sender, EventArgs e)//определитель1
        {
            int n = dataGridView1.RowCount;
            int m = dataGridView1.ColumnCount;
            int[][] c = new int[n][];
            for (int i = 0; i < n; i++)
            {
                c[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    c[i][j] = Convert.ToInt32(dataGridView1[i, j].Value);
                }
            }
            int q = Det(c);
            string q1 = Convert.ToString(q);
            MessageBox.Show(q1, "Определитель матрицы:");
        }

        private void button4_Click(object sender, EventArgs e) //ранг1
        {
            int n = dataGridView1.RowCount;
            int[,] c = new int[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i,j] = Convert.ToInt32(dataGridView1[i, j].Value);
                }
            }
            int rang = Rank(c,n);
            string rang1 = Convert.ToString(rang);
            MessageBox.Show(rang1, "Ранг матрицы:");

        }

        private void button5_Click(object sender, EventArgs e)//Умножить на число1
        {
            try
            {
                int n = dataGridView1.RowCount;
                int m = dataGridView1.ColumnCount;
                int x = int.Parse(textBox3.Text);
                dataGridView3.RowCount = n;
                dataGridView3.ColumnCount = m;
                int[,] a = new int[n, m];
                int[,] c = new int[n, m];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        a[i, j] = Convert.ToInt32(dataGridView1[i, j].Value);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        c[i, j] = a[i, j] * x;
                        dataGridView3[i, j].Value = c[i, j];
                    }
            }
            catch
            {
                MessageBox.Show("Введите число напротив умножения", "Ошибка ввода");
            }
        }

        private void button6_Click(object sender, EventArgs e)//Обратная матрица1
        {
            Powmatrix(-1);
        }

        private void button7_Click(object sender, EventArgs e)// Транспонирование матрицы1
        {
            int n = dataGridView1.RowCount;
            int m = dataGridView1.ColumnCount;
            dataGridView3.RowCount = n;
            dataGridView3.ColumnCount = m;
            double[,] a = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    a[i, j] = Convert.ToDouble(dataGridView1[i, j].Value);
                }

            double[,] matrixT = new double[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrixT[i, j] = a[j, i];
                }
            }

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    dataGridView3[i, j].Value = matrixT[i, j];
                }

        }

        private void button8_Click(object sender, EventArgs e)//Степепнь1
        {
            try
            {
                double x = double.Parse(textBox4.Text);
                Powmatrix(x);
            }
            catch
            {
                MessageBox.Show("Введите число напротив степени", "Ошибка ввода");
            }
        }

        private void button9_Click(object sender, EventArgs e)//Сложение 2х
        {
            try
            {
                if (textBox1.Text == textBox2.Text)
                {
                    int n;
                    int.TryParse(textBox2.Text, out n);
                    dataGridView3.RowCount = n;
                    dataGridView3.ColumnCount = n;
                    int[,] a = new int[n, n];
                    int[,] b = new int[n, n];
                    int[,] c = new int[n, n];
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            a[i, j] = Convert.ToInt32(dataGridView1[i, j].Value);
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            b[i, j] = Convert.ToInt32(dataGridView2[i, j].Value);
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                        {
                            c[i, j] = a[i, j] + b[i, j];
                            dataGridView3[i, j].Value = c[i, j];
                        }
                }
                else
                    MessageBox.Show("Складывать можно только матрицы одинакового размера");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода");
            }
        }

        private void button10_Click(object sender, EventArgs e)//Вычитание 2х
        {
            try
            {
                if (textBox1.Text == textBox2.Text)
                {
                    int n;
                    int.TryParse(textBox2.Text, out n);
                    dataGridView3.RowCount = n;
                    dataGridView3.ColumnCount = n;
                    int[,] a = new int[n, n];
                    int[,] b = new int[n, n];
                    int[,] c = new int[n, n];
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            a[i, j] = Convert.ToInt32(dataGridView1[i, j].Value);
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                            b[i, j] = Convert.ToInt32(dataGridView2[i, j].Value);
                    for (int i = 0; i < n; i++)
                        for (int j = 0; j < n; j++)
                        {
                            c[i, j] = a[i, j] - b[i, j];
                            dataGridView3[i, j].Value = c[i, j];
                        }
                }
                else
                    MessageBox.Show("Ошибка", "Вычитать можно только матрицы одинакового размера");
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода");
            }
        }

        private void button11_Click(object sender, EventArgs e)//Умножение 2х
        {
            if (textBox1.Text == textBox2.Text)
            {
                int n, v;
                int.TryParse(textBox2.Text, out n);
                dataGridView3.RowCount = n;
                dataGridView3.ColumnCount = n;
                int[,] a = new int[n, n];
                int[,] b = new int[n, n];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        a[j, i] = Convert.ToInt32(dataGridView1[i, j].Value);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        b[j, i] = Convert.ToInt32(dataGridView2[i, j].Value);

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        v = 0;
                        for (int r = 0; r < n; r++)
                            v += a[i, r] * b[r, j];
                        dataGridView3[i, j].Value = v;

                    }
                }
            }

            else
                MessageBox.Show("Ошибка ввода", "Умножать матрицы можно только когда количество столбцов первой матрицы равно количеству строк второй матрицы");
        }

        private void button17_Click(object sender, EventArgs e)//Определитель2
        {
            int n = dataGridView2.RowCount;
            int m = dataGridView2.ColumnCount;
            int[][] c = new int[n][];
            for (int i = 0; i < n; i++)
            {
                c[i] = new int[n];
                for (int j = 0; j < n; j++)
                {
                    c[i][j] = Convert.ToInt32(dataGridView2[i, j].Value);
                }
            }
            int q = Det(c);
            string q2 = Convert.ToString(q);
            MessageBox.Show(q2, "Определитель матрицы:");
        }

        private void button16_Click(object sender, EventArgs e)//Ранг2
        {
            int n = dataGridView2.RowCount;
            int[,] c = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    c[i, j] = Convert.ToInt32(dataGridView2[i, j].Value);
                }
            }
            int rang = Rank(c, n);
            string rang2 = Convert.ToString(rang);
            MessageBox.Show(rang2, "Ранг матрицы:");
        }

        private void button15_Click(object sender, EventArgs e)//Умножить на число2
        {
            try
            {
                int n = dataGridView2.RowCount;
                int m = dataGridView2.ColumnCount;
                int x = int.Parse(textBox6.Text);
                dataGridView3.RowCount = n;
                dataGridView3.ColumnCount = m;
                int[,] a = new int[n, m];
                int[,] c = new int[n, m];
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                        a[i, j] = Convert.ToInt32(dataGridView2[i, j].Value);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < m; j++)
                    {
                        c[i, j] = a[i, j] * x;
                        dataGridView3[i, j].Value = c[i, j];
                    }
            }
            catch
            {
                MessageBox.Show("Введите число напротив умножения", "Ошибка ввода");
            }
        }

        private void button14_Click(object sender, EventArgs e)//Обратная матрица2
        {
            Powmatrix(-1);
        }

        private void button13_Click(object sender, EventArgs e)// Транспонирование матрицы2
        {
            int n = dataGridView2.RowCount;
            int m = dataGridView2.ColumnCount;
            dataGridView3.RowCount = n;
            dataGridView3.ColumnCount = m;
            double[,] a = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    a[i, j] = Convert.ToDouble(dataGridView2[i, j].Value);
                }

            double[,] matrixT = new double[n, m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrixT[i, j] = a[j, i];
                }
            }

            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                {
                    dataGridView3[i, j].Value = matrixT[i, j];
                }

        }

        private void button12_Click(object sender, EventArgs e)//Степень2
        {
            try
            {
                double x = double.Parse(textBox5.Text);
                Powmatrix(x);
            }catch 
            {
                MessageBox.Show("Введите число напротив степени", "Ошибка ввода");
            }
        }
    }
    }
    

