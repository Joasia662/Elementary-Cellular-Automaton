using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace automata_One_dimensional
{
    public partial class Form1 : Form
    {

		//***************************** LOGIC *************************************
		static string convert_to_binary(String input)
		{
			string result;

			int num = Convert.ToInt32(input);
			result = "";
			while (num > 1)
			{
				int remainder = num % 2;
				result = Convert.ToString(remainder) + result;
				num /= 2;
			}
			result = Convert.ToString(num) + result;


			return result;
		}

		static DataTable get_DataTable_of_binary_rule(string output)
		{
			DataTable rule_binary = new System.Data.DataTable();
			rule_binary.Columns.Add("numbers", typeof(Int32));

			int temp_int_to_make_right_binary_format = 8;
			while (output.Length != temp_int_to_make_right_binary_format)
			{
				rule_binary.Rows.Add(0);
				temp_int_to_make_right_binary_format--;
			}

			for (int i = 0; i < output.Length; i++)
			{
				rule_binary.Rows.Add(Convert.ToInt32(new string(output[i], 1)));
			}


			

			return rule_binary;
		}


		static int parent_values_into_child(int one, int two, int three)
		{

			if (!(one < 0 || one > 1 || two < 0 || two > 1 || three < 0 || three > 1))
			{
				string status = Convert.ToString(one) + Convert.ToString(two) + Convert.ToString(three);
				int output = Convert.ToInt32(status, 2);
				return output;
			}
			else
			{
				//nothing 
				return 0;
			}

		}

		static int[,] createArray(int y, int x)
		{

			int[,] initMatrix = new int[y, x];
			for (int i = 0; i < y; i++)
			{
				for (int j = 0; j < x; j++)
				{
					initMatrix[i, j] = 0;
				}
			}

			int mid = x / 2;
			initMatrix[0, mid] = 1;
			return initMatrix;
		}



		//****************************END OF LOGIC ************************************************

		public Form1()
		{

			InitializeComponent();

			downUP_width.Maximum = 450;
			downUp_iteration_count.Maximum = 400;

			downUP_width.Minimum = 0;
			downUp_iteration_count.Minimum = 0;

			downUP_width.Increment = 1;
			downUp_iteration_count.Increment = 1;

			downUP_width.Value = 100;
			downUp_iteration_count.Value = 100;

			
		}


        private void Start_Click(object sender, EventArgs e)
        {



			int width = Convert.ToInt32(downUP_width.Value);
			int iteration_count = Convert.ToInt32(downUp_iteration_count.Value);
			string rule_dec = cb_rodzaj.SelectedItem.ToString();


			if (rule_dec == "30") { rule_dec = 120.ToString(); }
			else if (rule_dec == "120") { rule_dec = 30.ToString(); }

			string rule_bin = convert_to_binary(rule_dec);
			DataTable rule_numbers = get_DataTable_of_binary_rule(rule_bin);


			int[,] my_matrix = createArray(iteration_count, width);

			for (int i = 0; i < iteration_count - 1; i++)
			{
				for (int j = 0; j < width; j++)
				{


					int left = ((j == 0) ? (width - 1) : (j - 1));
					int right = ((j == (width - 1)) ? (0) : (j + 1));

					int child_index = parent_values_into_child(my_matrix[i, left], my_matrix[i, j], my_matrix[i, right]);
					my_matrix[i + 1, j] = Convert.ToInt32(rule_numbers.Rows[child_index]["numbers"]);
				}
			}

			int x = (450 - width) / 2;
			int y = 0;
			pic.Image = new Bitmap(pic.Width, pic.Height);
			for (int i = 0; i < my_matrix.GetUpperBound(0) + 1; i++)
				{
					for (int j = 0; j <= my_matrix.GetUpperBound((my_matrix.Rank) - 1); j++)
					{
						int value = my_matrix[i, j];
						if (value == 1)
						{
							((Bitmap)pic.Image).SetPixel(x, y, Color.FromArgb(0, 0, 0));
						}

						x++;
					}
					y++;
				x = (450 - width) / 2;
			}
		}

			




	}




}

        
        
    

