using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLight1.Model
{
    public class main
    {

        public void TestExport(int[] Export)
        {

            for (int p = 0; p < 8; p++)
            {
                for (int q = 0; q < p; q++)
                {
                    if ((Export[p] != 8) && (Export[p] == Export[q]))
                    { Export[p] = 8; }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                if (Export[i] >= 8)
                {
                    Export[i]--;
                    {

                        for (int j = 0; j < 8; j++)
                        {
                            if (i != j && Export[i] == Export[j])
                            {
                                Export[i]--;
                                j = -1;
                            }
                        }
                    }
                }
            }
        }
        // Export目的地址
        // Import源地址
        public void SetSwitch(int[] Export, int[] Import, Switch[,] sw, int k,
                int[,] tag, int i)

	{
		int []sign1 = new int[8];
		int temp = 0;
		int temp2 = 0;
		int a = 0;

		 while (a != -1) {
			if (Export[a] == 100) {

				while (Export[a] == 100) {
					a++;
					if (a == 8)
						break ;
				}
			}

			if (Export[a] % 2 == 0)
				sw[Export[a] / 2,k].setFlag(0);
			else
				sw[Export[a] / 2,k].setFlag(1);

			if (Export[a] % 2 == 0) {
				if (Import[Export[a] + 1] % 2 == 0)
					sw[Import[Export[a] + 1] / 2,4 - k].setFlag(1);
				else
					sw[Import[Export[a] + 1] / 2,4 - k].setFlag(0);

				if (Import[Export[a] + 1] % 2 == 0)

					a = Import[Export[a] + 1] + 1;
				else
					a = Import[Export[a] + 1] - 1;

			}

			else {
				if (Import[Export[a] - 1] % 2 == 0)
					sw[Import[Export[a] - 1] / 2,4 - k].setFlag(1);

				else
					sw[Import[Export[a] - 1] / 2,4 - k].setFlag(0);

				if (Import[Export[a] - 1] % 2 == 0)

					a = Import[Export[a] - 1] + 1;
				else

					a = Import[Export[a] - 1] - 1;
			}

			if (sw[Export[a] / 2,k].getFlag() != -1) {
				if (Search(sw, k, i, Export, Import) == -1)
					a = -1;
				else
					// if(Import[2 * Search(sw, k, i, Export, Import)]%2==0)
					a = 2 * Search(sw, k, i, Export, Import);
				// else a=Import[2 * Search(sw, k, i, Export, Import)]-1;
			}
		}

		for (int p = 0; p < Export.Length / 2; p++) {

			if (sw[p,k].getFlag() == 1
					&& (Export[2 * p] != 100 && Import[2 * p] != 100)) {

				temp2 = Export[Import[2 * p]];
				Export[Import[2 * p]] = Export[Import[2 * p + 1]];
				Export[Import[2 * p + 1]] = temp2;
			}
		}
		for (int p = 0; p < Export.Length / 2; p++) {
			if (sw[p,4 - k].getFlag() == 1
					&& (Export[2 * p] != 100 && Import[2 * p] != 100)) {
				temp = Export[2 * p + 1];
				Export[2 * p + 1] = Export[2 * p];
				Export[2 * p] = temp;
			}
		}

		for (int p = 0; p < Export.Length; p++) {
			if (Export[p] != 100)
				Import[Export[p]] = p;
		}

        int[] sign3 = (int[])Export.Clone();
		for (int p = 0; p < Export.Length; p++) {
			if (Import[tag[k + 1,p]] != 100 && Import[p] != 100)
				Export[Import[tag[k + 1,p]]] = sign3[Import[p]];
		}

		sign1 = (int [])Export.Clone();

		for (int p = 0; p < Export.Length; p++) {
			if (Export[p] != 100 && Import[p] != 100)
				Export[p] = sign1[tag[k + 1,p]];
		}

		for (int p = 0; p < Export.Length; p++) {
			if (Export[p] != 100 && Import[p] != 100)
				Import[Export[p]] = p;
		}
		if (i == 1) {
			for (int p = 0; p < 8; p++) {
				if (Export[p] != 100 && Import[p] != 100) {
					if (Export[Import[p]] == p)
						sw[p / 2,k].setFlag(0);
					if (Export[Import[p]] != p)
						sw[p / 2,k].setFlag(1);
				}
			}
		}

		if (i > 1) {
			int[] export1 = new int[8];
			int[] export2 = new int[8];
			int[] import1 = new int[8];
			int[] import2 = new int[8];
			for (int p = 0; p < export2.Length; p++) {
				export2[p] = 100;
				import2[p] = 100;
				export1[p] = 100;
				import1[p] = 100;
			}

			int y = 0;
			for (int p = 0; p < 8; p++) {
				if (Export[p] != 100 && Import[p] != 100 && y < i) {
					export1[p] = Export[p];
					export2[p + i] = Export[p + i];
					import1[p] = Import[p];
					import2[p + i] = Import[p + i];
					y++;
				}

			}

			SetSwitch(export1, import1, sw, k + 1, tag, i / 2);
			SetSwitch(export2, import2, sw, k + 1, tag, i / 2);

		}
	}

        public static int Search(Switch[,] sw, int k, int i, int[] Export,
                int[] Import)
        {
            int j = 0;

            while ((sw[j,4 - k].getFlag() != -1) || (Export[2 * j] == 100)
                    || (Import[2 * j] == 100))
            {
                j++;

                {
                    if (j == 4)
                        break;
                }

            }

            if (j == 4)
                return -1;
            else
                return j;
        }

    }

}
