
class Program
{
    static void Main(String[] args)
    {
        int[] Board = new int[82]; //Suduko Board
        int[] Box = new int[10]; //get numbers in box and check on them
        int place = 1;
        int Pre_Numbers;//Total number of numbers that are already given
        int position; //position numbers will be placed example 1 is top left first box and we move from left to right then once we reach end of line we go to next line from left to right

        int Number;
        Console.WriteLine("How many Pre Given numbers are there?");
        Pre_Numbers = Convert.ToInt32(Console.ReadLine());
        for (int i = 0; i < Pre_Numbers; i++)
        {
            Console.Write("Number: ");
            Number = Convert.ToInt32(Console.ReadLine());
            Console.Write("Position: ");
            position = Convert.ToInt32(Console.ReadLine());
            Board[position] = Number;
        }
        solver solve = new solver();
        if (solve.solve_board(Board, place, 1) == 1)
        {
            //show board
            for (int i = 1; i < 82; i++)
            {
                if (i % 9 != 0)
                {
                    Console.Write(Board[i]);
                    Console.Write("|");
                }
                else if (i % 9 == 0 && i != 1)
                {
                    Console.Write(Board[i]);
                    Console.WriteLine();
                    if (i != 81)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Console.Write("--");
                        }
                        Console.WriteLine();
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No Solution exists");
        }
    }
}

class solver
{
    public solver() { 
    
    }
    public int solve_board(int[] Board, int place, int Number)
    {
        int counter_x = 1;//Used To make sure whole row is checked
        int check_position = place + 1;//used to choose row elements
        int counter_y = 1;//Used To make sure whole row is checked
        int Num_above_it = 0;//used to count how many numbers above it to see which box it belongs to
        int max_place_box = place; //used to find range of place in that box

        //base case
        if (place == 82)
        {
            return 1;
        }
        else
        {
            //it is empty
            if (Board[place] == 0)
            {
                Board[place] = Number;
                //while it is wrong
                while(check_row(counter_x, check_position, place, Board) || check_column(counter_y, check_position, place, Board) || check_box(check_position, max_place_box, place, Num_above_it, Board)){
                    Number++;
                    if(Number >= 10)
                    {
                        Board[place] = 0;
                        return 0;
                    }
                    Board[place] = Number;
                }
                //no error found
                Board[place] = Number;
                //The number after it is impossible should backtrack
                if (solve_board(Board, place + 1, 1) == 0)
                {
                    Number++;
                    Board[place] = 0;
                    //incase number was 9 for one before
                    if (Number == 10)
                    {
                        return 0;
                    }
                    else
                    {
                       return solve_board(Board, place, Number);
                    }
                }
                //it worked should continue
                else
                {
                    return 1;
                }
            }
            //skip it and go to next one
            else
            {
                return solve_board(Board, place + 1,1);
            }
        }
    }

    private bool check_row(int counter_x, int check_position, int place, int[] Board)
    {
        //make it first element in row
        if(place % 9 == 0)//last element in row
        {
            check_position = place - 8;
        }
        else
        {
            check_position = place + 1;   
        }
        while (counter_x < 8)
        {
            //error found
            if (Board[place] == Board[check_position])
            {
                return true;
            }
            if (check_position % 9 == 0)
            {
                check_position -= 8;
            }
            else
            {
                check_position++;
            }
            counter_x++;
        }
        return false;
    }
    private bool check_column(int counter_y, int check_position, int place, int[] Board)
    {
        check_position = place + 9;
        if (place >= 73 && place <= 81)//check if last row was reached
        {
            while (check_position > 9)
            {
                check_position -= 9;
            }
        }
        counter_y = 1;
        //check for column
        while (counter_y < 9)
        {
            if (check_position >= 73 && check_position <= 81)//check if last row was reached
            {
                while (check_position > 9)
                {
                    check_position -= 9;
                }
            }
            if (Board[place] == Board[check_position] && place != check_position)
            {
                Console.WriteLine("BROKE IN Column");
                return true;
            }
            check_position += 9;
            counter_y++;
        }
        return false;
    }
    private bool check_box(int check_position, int max_place_box, int place, int Num_above_it, int[] Board)
    {
        bool error_box = false;
        int column = place;//will be used to set it to first row first column of box
        //for 3x3 box
        check_position = place - 9;
        while (check_position > 0)
        {
            check_position -= 9;
            Num_above_it++;
        }
        check_position += 9;

        if (Num_above_it % 3 == 0)//first row of box
        {
            if (check_position % 3 == 0)//third element in row
            {
                column -= 2;
            }
            else if (check_position % 3 == 2) //second element in row
            {
                column -= 1;
            }
        }

        else if ((Num_above_it % 3) == 1) // second row
        {
            if (check_position % 3 == 0)//third element in row
            {
                column -= 11;
            }
            else if (check_position % 3 == 2) //second element in row
            {
                column -= 10;
            }
            else //first element in row
            {
                column -= 9;
            }
        }
        else
        {
            if (check_position % 3 == 0)//third element in row
            {
                column -= 20;
            }
            else if (check_position % 3 == 2) //second element in row
            {
                column -= 19;
            }
            else
            {
                column -= 18;
            }
        }
        for(int i = column;i < column + 3; i++)
        {
            if (Board[i] == Board[place] && i != place)
            {
                Console.WriteLine("Broke in Box");
                error_box = true;
            }
        }
        for(int i = column;i < column + 9 * 3;i += 9)
        {
            if (Board[i] == Board[place] && i != place)
            {
                Console.WriteLine("Broke in Box");
                error_box = true;
            }
        }
        for (int i = column + 1; i < column + 9 * 3; i += 9)
        {
            if (Board[i] == Board[place] && i != place)
            {
                Console.WriteLine("Broke in Box");
                error_box = true;
            }
        }
        for (int i = column + 2; i < column + 9 * 3; i += 9)
        {
            if (Board[i] == Board[place] && i != place)
            {
                Console.WriteLine("Broke in Box");
                error_box = true;
            }
        }
        return error_box;
    }
}