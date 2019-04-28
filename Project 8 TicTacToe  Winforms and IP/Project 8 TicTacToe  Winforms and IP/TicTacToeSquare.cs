using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_8_TicTacToe__Winforms_and_IP
{
    enum SQUARE_TYPE { EMPTY, X, Y }

    class TicTacToeSquare
    {
        public SQUARE_TYPE value;

        public TicTacToeSquare()
        {
            value = SQUARE_TYPE.EMPTY;
        }
    }
}
