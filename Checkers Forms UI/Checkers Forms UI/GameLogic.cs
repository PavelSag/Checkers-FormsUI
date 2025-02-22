using System;
using System.Collections.Generic;

namespace Checkers
{
    internal class GameLogic
    {
        private bool m_IsGameGoing = true;
        private OriginAndDestinationSquare m_PlayerMove;
        private bool m_IsForfeited = false;
        private bool m_IsAnotherGame = false;
        private bool m_IsAnotherCapture = true;
        private Player m_Winner = null;
        private Player m_Loser = null;
        internal static eTurnStatus s_TurnStatus = eTurnStatus.Player1Turn;
        internal static bool s_ComputerIndicator = false;

        public GameLogic()
        {

        }

        public Player Winner
        {
            get { return m_Winner; }
            set
            {
                m_Winner = value;
            }

        }

        public Player Loser
        {
            get { return m_Loser; }
            set
            {
                m_Loser = value;
            }

        }

        public bool IsAnotherCapture
        {
            get { return m_IsAnotherCapture; }
            set
            {
                m_IsAnotherCapture = value;
            }
        }

        public bool IsGameGoing
        {
            get { return m_IsGameGoing; }
            set
            {
                m_IsGameGoing = value;
            }
        }

        public OriginAndDestinationSquare PlayerMove
        {
            get { return m_PlayerMove; }
            set
            {
                m_PlayerMove = value;
            }
        }

        public bool IsForfeited
        {
            get { return m_IsForfeited; }
            set
            {
                m_IsForfeited = value;
            }
        }

        public bool IsAnotherGame
        {
            get { return m_IsAnotherGame; }
            set
            {
                m_IsAnotherGame = value;
            }
        }

        internal bool CheckIfLegalAndMove(ref bool io_IsPieceCaptured, Board i_Board, OriginAndDestinationSquare i_FromAndWhere, out string o_IllegalMoveMessage, List<OriginAndDestinationSquare> i_CapturesArray)
        {
            o_IllegalMoveMessage = null;
            bool isLegalMove = true;
            bool isCaptureChosen = false;

            if (isLegalMove == true)
            {
                isLegalMove = checkIfLegal(i_FromAndWhere, i_Board, out o_IllegalMoveMessage);
            }

            List<OriginAndDestinationSquare> captureOptionsArray = new List<OriginAndDestinationSquare>();
            bool isThereACapture = giveArrayOfCaptureOptionsFromWholeBoard(captureOptionsArray, i_Board);

            if (isThereACapture == true && isCaptureChosen != true)
            {
                for (int i = 0; i < captureOptionsArray.Count; i++)
                {
                    if (captureOptionsArray[i].Equals(i_FromAndWhere))
                    {
                        isCaptureChosen = true;
                    }
                }

                if (isCaptureChosen == false)
                {
                    isLegalMove = false;
                    o_IllegalMoveMessage = "Illegal Move! If you have a capture available, you must capture";
                }
            }

            if (isLegalMove == true)
            {
                io_IsPieceCaptured = MovePiece(i_FromAndWhere, i_Board);
            }

            if(o_IllegalMoveMessage == null)
            {
                o_IllegalMoveMessage = "Illegal Move!";
            }

            return isLegalMove;
        }

        internal List<OriginAndDestinationSquare> GetArrayOfAllPossibleMovesOnBoard(List<OriginAndDestinationSquare> i_PossibleMoves, Board i_Board, eSquareOwner i_SquareOwner)
        {
            int[,] directions = { { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 } };
            giveArrayOfCaptureOptionsFromWholeBoard(i_PossibleMoves, i_Board);

            for (int row = 0; row < i_Board.BoardSize; row++)
            {
                for (int col = 0; col < i_Board.BoardSize; col++)
                {
                    if (i_Board.GameBoard[row, col].SquareOwner == i_SquareOwner)
                    {
                        for (int i = 0; i < directions.GetLength(0); i++)
                        {
                            int newRow = row + directions[i, 0];
                            int newCol = col + directions[i, 1];
                            OriginAndDestinationSquare possibleMove = new OriginAndDestinationSquare();
                            possibleMove.m_RowOrigin = row;
                            possibleMove.m_ColOrigin = col;
                            possibleMove.m_RowDestination = newRow;
                            possibleMove.m_ColDestination = newCol;

                            string illegalMessage = null;
                            if(checkIfLegal(possibleMove, i_Board, out illegalMessage) == true)
                            {
                                i_PossibleMoves.Add(possibleMove);
                            }
                        }
                    }
                }
            }

            return i_PossibleMoves;
        }

        private bool giveArrayOfCaptureOptionsFromWholeBoard(List<OriginAndDestinationSquare> i_CaptureOptionsArray, Board i_Board)
        {
            bool returnFlag = false;
            OriginAndDestinationSquare fromAndToSquare = new OriginAndDestinationSquare();

            for (int i = 0; i < i_Board.BoardSize; i++)
            {
                for (int j = 0; j < i_Board.BoardSize; j++)
                {
                    fromAndToSquare.m_RowOrigin = i;
                    fromAndToSquare.m_ColOrigin = j;
                    giveArrayOfCaptureOptionsFromOneSquare(i_CaptureOptionsArray, fromAndToSquare, i_Board);
                }
            }

            if (i_CaptureOptionsArray.Count == 0)
            {
                returnFlag = false;
            }
            else
            {
                returnFlag = true;
            }

            return returnFlag;
        }

        private bool checkIfLegal(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board, out string o_IllegalMoveMessage)
        {
            bool returnFlag = true;
            o_IllegalMoveMessage = null;

            returnFlag = CheckBoundaries(i_FromAndToSquare, i_Board.BoardSize);
            if(returnFlag != true)
            {
                o_IllegalMoveMessage = "Illegal Move! Your move is outside the boundaries of the board";
            }
            else
            {
                returnFlag = CheckIfDiagonalIsValid(i_FromAndToSquare, i_Board, out o_IllegalMoveMessage);

                if(returnFlag == true)
                {
                    returnFlag = checkIfPlayerMovesOthersPiece(i_FromAndToSquare, i_Board);
                    if(returnFlag != true)
                    {
                        o_IllegalMoveMessage = "IllegalMove! You can only move your own pieces";
                    }
                }
            }

            if(o_IllegalMoveMessage == null)
            {
                o_IllegalMoveMessage = "Illegal Move!";
            }

            return returnFlag;
        }

        internal bool CheckBoundaries(OriginAndDestinationSquare i_FromAndToSquare, int i_BoardSize)
        {
            bool returnFlag = false;

            if (i_FromAndToSquare.m_RowOrigin >= 0 && i_FromAndToSquare.m_RowOrigin < i_BoardSize &&
                i_FromAndToSquare.m_ColOrigin >= 0 && i_FromAndToSquare.m_ColOrigin < i_BoardSize &&
                i_FromAndToSquare.m_RowDestination >= 0 && i_FromAndToSquare.m_RowDestination < i_BoardSize &&
                i_FromAndToSquare.m_ColDestination >= 0 && i_FromAndToSquare.m_ColDestination < i_BoardSize)
            {
                returnFlag = true;
            }

            return returnFlag;
        }

        internal bool CheckIfDiagonalIsValid(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board, out string o_IllegalMoveMessage)
        {
            o_IllegalMoveMessage = null;
            bool returnFlag = true;
            Square originSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin];
            Square destinationSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination];

            if (destinationSquare.PieceInSquare != ePieceInSquare.Empty || destinationSquare.SquareOwner == originSquare.SquareOwner)
            {
                returnFlag = false;
                o_IllegalMoveMessage = "Illegal Move! You must move your own piece and to an empty square";
            }
            else
            {
                returnFlag = checkCapture(i_FromAndToSquare, i_Board);
                if(returnFlag != true)
                {
                    o_IllegalMoveMessage = "Illegal Move! You must capture an enemy piece when you have the opportunity and if you don't" +
                        "have a capture, move diagonally in your direction";
                }
                else
                {
                    returnFlag = checkKingMove(i_FromAndToSquare, i_Board, out o_IllegalMoveMessage);
                }
            }

            if(o_IllegalMoveMessage == null)
            {
                o_IllegalMoveMessage = "Invalid Move!";
            }

            return returnFlag;
        }

        private bool checkCapture(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            bool returnFlag = true;

            Square originSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin];
            Square destinationSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination];
            int rowDiff = Math.Abs(i_FromAndToSquare.m_RowDestination - i_FromAndToSquare.m_RowOrigin);
            int colDiff = Math.Abs(i_FromAndToSquare.m_ColDestination - i_FromAndToSquare.m_ColOrigin);

            if (rowDiff == 2 && colDiff == 2)
            {
                int midRow = (i_FromAndToSquare.m_RowOrigin + i_FromAndToSquare.m_RowDestination) / 2;
                int midCol = (i_FromAndToSquare.m_ColOrigin + i_FromAndToSquare.m_ColDestination) / 2;
                Square midSquare = i_Board.GameBoard[midRow, midCol];

                if (midSquare.SquareOwner != originSquare.SquareOwner && midSquare.SquareOwner != eSquareOwner.None)
                {
                    returnFlag = true; ;
                }
                else
                {
                    returnFlag = false;
                }
            }
            else
            {
                if (originSquare.SquareOwner == destinationSquare.SquareOwner)
                {
                    returnFlag = false;
                }
            }

            return returnFlag;
        }

        private bool checkKingMove(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board, out string o_IllegalMoveMessage)
        {
            o_IllegalMoveMessage = null;
            bool returnFlag = true;
            Square originSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin];
            int rowDiff = Math.Abs(i_FromAndToSquare.m_RowDestination - i_FromAndToSquare.m_RowOrigin);
            int colDiff = Math.Abs(i_FromAndToSquare.m_ColDestination - i_FromAndToSquare.m_ColOrigin);

            if (originSquare.PieceInSquare == ePieceInSquare.KingPiece)
            {
                if ((rowDiff == 1 && colDiff == 1) || (rowDiff == 2 && colDiff == 2))
                {
                    returnFlag = true;
                }
                else
                {
                    returnFlag = false;
                    o_IllegalMoveMessage = "Illegal Move! You have a move with the king";
                }
            }
            else
            {
                bool isMovingForward = (originSquare.SquareOwner == eSquareOwner.Player1 && i_FromAndToSquare.m_RowDestination < i_FromAndToSquare.m_RowOrigin) ||
                                       (originSquare.SquareOwner == eSquareOwner.Player2 && i_FromAndToSquare.m_RowDestination > i_FromAndToSquare.m_RowOrigin);

                if (isMovingForward == true && rowDiff <= 2 && colDiff <= 2)
                {
                    returnFlag = true;
                }
                else
                {
                    o_IllegalMoveMessage = "Illegal Move! You must capture an enemy piece when you have the opportunity and if you don't" +
                        "have a capture, move diagonally in your direction";
                    returnFlag = false;
                }
            }

            if(o_IllegalMoveMessage == null)
            {
                o_IllegalMoveMessage = "Illegal Move!";
            }

            return returnFlag;
        }

        private bool checkIfPlayerMovesOthersPiece(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            bool returnFlag = true;
            Square originSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin];

            if (originSquare.SquareOwner == eSquareOwner.Player1 && s_TurnStatus != eTurnStatus.Player1Turn)
            {
                returnFlag = false;
            }
            else if (originSquare.SquareOwner == eSquareOwner.Player2 && s_TurnStatus != eTurnStatus.Player2Turn)
            {
                returnFlag = false;
            }
            else if (originSquare.SquareOwner == eSquareOwner.None)
            {
                returnFlag = false;
            }

            return returnFlag;
        }

        internal bool MovePiece(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            bool isPieceCaptured = false;
            updateNewSquareAndEmptyPrevious(i_FromAndToSquare, i_Board);
            isPieceCaptured = updateCapture(i_FromAndToSquare, i_Board);
            updateKing(i_FromAndToSquare, i_Board);

            return isPieceCaptured;
        }

        private void updateNewSquareAndEmptyPrevious(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination].PieceInSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin].PieceInSquare;
            i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination].SquareOwner = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin].SquareOwner;
            i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin].PieceInSquare = ePieceInSquare.Empty;
            i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin].SquareOwner = eSquareOwner.None;
        }

        private bool updateCapture(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            bool isPieceCaptured = false;

            if (Math.Abs(i_FromAndToSquare.m_RowDestination - i_FromAndToSquare.m_RowOrigin) == 2 && Math.Abs(i_FromAndToSquare.m_ColDestination - i_FromAndToSquare.m_ColOrigin) == 2)
            {
                int midRow = (i_FromAndToSquare.m_RowOrigin + i_FromAndToSquare.m_RowDestination) / 2;
                int midCol = (i_FromAndToSquare.m_ColOrigin + i_FromAndToSquare.m_ColDestination) / 2;
                Square midSquare = i_Board.GameBoard[midRow, midCol];
                midSquare.PieceInSquare = ePieceInSquare.Empty;
                midSquare.SquareOwner = eSquareOwner.None;
                isPieceCaptured = true;
            }

            return isPieceCaptured;
        }

        private void updateKing(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            Square destinationSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination];

            if ((destinationSquare.SquareOwner == eSquareOwner.Player1 && i_FromAndToSquare.m_RowDestination == 0) ||
                    (destinationSquare.SquareOwner == eSquareOwner.Player2 && i_FromAndToSquare.m_RowDestination == i_Board.BoardSize - 1))
            {
                destinationSquare.PieceInSquare = ePieceInSquare.KingPiece;
                i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination].PieceInSquare = ePieceInSquare.KingPiece;
            }
        }

        internal void DecrementNumOfPieces(Player i_Player1, Player i_Player2)
        {
            if (s_TurnStatus == eTurnStatus.Player2Turn || s_TurnStatus == eTurnStatus.ComputerTurn)
            {
                i_Player1.NumOfPiecesOnBoard--;
            }
            else
            {
                i_Player2.NumOfPiecesOnBoard--;
            }
        }

        internal void UpdateWinnerAndLoserIfVictory(Player i_Player1, Player i_Player2, Board i_Board)
        {
            string winnerIndicator = null;

            if (IsForfeited == true)
            {
                if (s_TurnStatus.Equals(eTurnStatus.Player1Turn))
                {
                    this.Winner = i_Player2;
                    this.Loser = i_Player1;
                }
                else
                {
                    this.Winner = i_Player1;
                    this.Loser = i_Player2;
                }
            }
            else
            {
                if (i_Player2.NumOfPiecesOnBoard == 0)
                {
                    this.Winner = i_Player1;
                    this.Loser = i_Player2;
                }
                else if (i_Player1.NumOfPiecesOnBoard == 0)
                {
                    this.Winner = i_Player2;
                    this.Loser = i_Player1;
                }
                else
                {
                    winnerIndicator = checkIfLegalMovesAreLeftForPlayer(i_Board);
                    if (winnerIndicator != null)
                    {
                        if (winnerIndicator == ("Player1"))
                        {
                            this.Winner = i_Player1;
                            this.Loser = i_Player2;
                        }
                        else
                        {
                            this.Winner = i_Player2;
                            this.Loser = i_Player1;
                        }
                    }
                }
            }
        }
        private string checkIfLegalMovesAreLeftForPlayer(Board i_Board)
        {
            string winner = null;
            bool isLegalMoveFoundForPlayer1 = false;
            bool isLegalMoveFoundForPlayer2 = false;
            int[,] directions = { { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 }, { -2, -2 }, { -2, 2 }, { 2, -2 }, { 2, 2 } };

            for (int row = 0; row < i_Board.BoardSize; row++)
            {
                for (int col = 0; col < i_Board.BoardSize; col++)
                {
                    for (int i = 0; i < directions.GetLength(0); i++)
                    {
                        int newRow = row + directions[i, 0];
                        int newCol = col + directions[i, 1];
                        OriginAndDestinationSquare fromAndToSquare = new OriginAndDestinationSquare();
                        fromAndToSquare.m_RowOrigin = row;
                        fromAndToSquare.m_ColOrigin = col;
                        fromAndToSquare.m_RowDestination = newRow;
                        fromAndToSquare.m_ColDestination = newCol;

                        if ((isLegalMoveFoundForPlayer1 == true && i_Board.GameBoard[row, col].SquareOwner == eSquareOwner.Player1)
                            || (isLegalMoveFoundForPlayer2 == true && i_Board.GameBoard[row, col].SquareOwner == eSquareOwner.Player2))
                        {
                            continue;
                        }

                        if (CheckBoundaries(fromAndToSquare, i_Board.BoardSize) == true)
                        {
                            string IllegalMoveMessage = null;
                            if ((CheckIfDiagonalIsValid(fromAndToSquare, i_Board, out IllegalMoveMessage) == true
                                || isCaptureMoveForComputer(fromAndToSquare, i_Board) == true))
                            {
                                if (i_Board.GameBoard[row, col].SquareOwner == eSquareOwner.Player1)
                                {
                                    isLegalMoveFoundForPlayer1 = true;
                                }
                                else if (i_Board.GameBoard[row, col].SquareOwner == eSquareOwner.Player2)
                                {
                                    isLegalMoveFoundForPlayer2 = true;
                                }
                            }
                        }
                    }
                }
            }

            if (isLegalMoveFoundForPlayer1 == true && isLegalMoveFoundForPlayer2 == true)
            {
                winner = null;
            }

            if (isLegalMoveFoundForPlayer2 == true && isLegalMoveFoundForPlayer1 != true && s_TurnStatus == eTurnStatus.Player2Turn)
            {
                winner = "Player2";
            }

            if (isLegalMoveFoundForPlayer1 == true && isLegalMoveFoundForPlayer2 != true && s_TurnStatus == eTurnStatus.Player1Turn)
            {
                winner = "Player1";
            }

            return winner;
        }

        private bool isCaptureMoveForComputer(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            bool returnFlag = false;
            int rowDiff = Math.Abs(i_FromAndToSquare.m_RowDestination - i_FromAndToSquare.m_RowOrigin);
            int colDiff = Math.Abs(i_FromAndToSquare.m_ColDestination - i_FromAndToSquare.m_ColOrigin);

            if (rowDiff == 2 && colDiff == 2)
            {
                int midRow = (i_FromAndToSquare.m_RowOrigin + i_FromAndToSquare.m_RowDestination) / 2;
                int midCol = (i_FromAndToSquare.m_ColOrigin + i_FromAndToSquare.m_ColDestination) / 2;
                Square originSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin];
                Square midSquare = i_Board.GameBoard[midRow, midCol];
                Square destinationSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination];

                if (midSquare.SquareOwner != originSquare.SquareOwner && midSquare.SquareOwner != eSquareOwner.None && destinationSquare.SquareOwner == eSquareOwner.None)
                {
                    if (originSquare.PieceInSquare == ePieceInSquare.RegularPiece)
                    {
                        int direction = i_FromAndToSquare.m_RowDestination - i_FromAndToSquare.m_RowOrigin;
                        if (originSquare.SquareOwner == eSquareOwner.Player2 && direction > 0)
                        {
                            returnFlag = true;
                        }
                    }
                    else if (originSquare.PieceInSquare == ePieceInSquare.KingPiece)
                    {
                        returnFlag = true;
                    }
                }
            }

            return returnFlag;
        }

        internal void ChangeTurn()
        {
            if (s_TurnStatus == eTurnStatus.Player1Turn)
            {
                s_TurnStatus = eTurnStatus.Player2Turn;
            }
            else
            {
                s_TurnStatus = eTurnStatus.Player1Turn;
            }
        }

        internal OriginAndDestinationSquare GenerateComputerMove(Board i_Board)
        {
            List<OriginAndDestinationSquare> optionsArray = GetOptionsArrayForComputer(i_Board);
            OriginAndDestinationSquare computerMove = chooseRandomFromOptionsArray(optionsArray, i_Board);
            return computerMove;
        }

        internal List<OriginAndDestinationSquare> GetOptionsArrayForComputer(Board i_Board)
        {
            List<OriginAndDestinationSquare> optionsArray = new List<OriginAndDestinationSquare>();
            int[,] directions = { { -1, -1 }, { -1, 1 }, { 1, -1 }, { 1, 1 }, { -2, -2 }, { -2, 2 }, { 2, -2 }, { 2, 2 } };

            for (int row = 0; row < i_Board.BoardSize; row++)
            {
                for (int col = 0; col < i_Board.BoardSize; col++)
                {
                    if (i_Board.GameBoard[row, col].SquareOwner == eSquareOwner.Player2)
                    {
                        for (int i = 0; i < directions.GetLength(0); i++)
                        {
                            int newRow = row + directions[i, 0];
                            int newCol = col + directions[i, 1];
                            OriginAndDestinationSquare fromAndToSquare = new OriginAndDestinationSquare();
                            fromAndToSquare.m_RowOrigin = row;
                            fromAndToSquare.m_ColOrigin = col;
                            fromAndToSquare.m_RowDestination = newRow;
                            fromAndToSquare.m_ColDestination = newCol;

                            if (CheckBoundaries(fromAndToSquare, i_Board.BoardSize) == true)
                            {
                                string illegalMoveMessage = null;
                                if (CheckIfDiagonalIsValid(fromAndToSquare, i_Board, out illegalMoveMessage) == true || isCaptureMoveForComputer(fromAndToSquare, i_Board) == true)
                                {
                                    optionsArray.Add(fromAndToSquare);
                                }
                            }
                        }
                    }
                }
            }

            return optionsArray;
        }

        private OriginAndDestinationSquare chooseRandomFromOptionsArray(List<OriginAndDestinationSquare> i_OptionsArray, Board i_Board)
        {
            OriginAndDestinationSquare originAndDestinationSquare = new OriginAndDestinationSquare();
            List<OriginAndDestinationSquare> capturesArray = new List<OriginAndDestinationSquare>();
            List<OriginAndDestinationSquare> makeKingArray = new List<OriginAndDestinationSquare>();
            OriginAndDestinationSquare chosenMove;
            Random random = new Random();
            int randomIndex = 0;

            if (i_OptionsArray.Count == 0)
            {
                originAndDestinationSquare.m_RowOrigin = -1;
                originAndDestinationSquare.m_ColOrigin = -1;
                originAndDestinationSquare.m_RowDestination = -1;
                originAndDestinationSquare.m_ColDestination = -1;

                return originAndDestinationSquare;
            }

            for (int i = 0; i < i_OptionsArray.Count; i++)
            {
                if (isCaptureMoveForComputer(i_OptionsArray[i], i_Board) == true)
                {
                    capturesArray.Add(i_OptionsArray[i]);
                }
            }

            for (int i = 0; i < i_OptionsArray.Count; i++)
            {
                if (checkIfComputerCanMakeKing(i_OptionsArray[i], i_Board) == true)
                {
                    makeKingArray.Add(i_OptionsArray[i]);
                }
            }

            if (capturesArray.Count != 0)
            {
                randomIndex = random.Next(capturesArray.Count);
                chosenMove = capturesArray[randomIndex];
            }
            else if (makeKingArray.Count != 0)
            {
                randomIndex = random.Next(makeKingArray.Count);
                chosenMove = makeKingArray[randomIndex];
            }
            else
            {
                randomIndex = random.Next(i_OptionsArray.Count);
                chosenMove = i_OptionsArray[randomIndex];
            }

            originAndDestinationSquare.m_ColOrigin = chosenMove.m_ColOrigin;
            originAndDestinationSquare.m_RowOrigin = chosenMove.m_RowOrigin;
            originAndDestinationSquare.m_ColDestination = chosenMove.m_ColDestination;
            originAndDestinationSquare.m_RowDestination = chosenMove.m_RowDestination;

            return originAndDestinationSquare;
        }

        private bool checkIfComputerCanMakeKing(OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            bool returnFlag = false;
            Square originSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowOrigin, i_FromAndToSquare.m_ColOrigin];
            Square destinationSquare = i_Board.GameBoard[i_FromAndToSquare.m_RowDestination, i_FromAndToSquare.m_ColDestination];

            if (originSquare.SquareOwner == eSquareOwner.Player2 && i_FromAndToSquare.m_RowDestination == i_Board.BoardSize - 1)
            {
                returnFlag = true;
            }
            else if (originSquare.SquareOwner == eSquareOwner.Player1 && i_FromAndToSquare.m_RowDestination == 0)
            {
                returnFlag = true;
            }

            return returnFlag;
        }

        internal bool FindDoubleCapture(Board i_Board, List<OriginAndDestinationSquare> i_CaptureOptionsArray, OriginAndDestinationSquare i_OldFromAndToSquare)
        {
            bool returnFlag = false;
            OriginAndDestinationSquare newFromAndToSquare = new OriginAndDestinationSquare();
            newFromAndToSquare.m_RowOrigin = i_OldFromAndToSquare.m_RowDestination;
            newFromAndToSquare.m_ColOrigin = i_OldFromAndToSquare.m_ColDestination;
            returnFlag = giveArrayOfCaptureOptionsFromOneSquare(i_CaptureOptionsArray, newFromAndToSquare, i_Board);
            return returnFlag;
        }

        private bool giveArrayOfCaptureOptionsFromOneSquare(List<OriginAndDestinationSquare> i_CaptureOptionsArray, OriginAndDestinationSquare i_FromAndToSquare, Board i_Board)
        {
            bool returnFlag = false;
            int[,] captureDirections = { { -2, -2 }, { -2, 2 }, { 2, -2 }, { 2, 2 } };

            for (int i = 0; i < captureDirections.GetLength(0); i++)
            {
                int potentialRow = i_FromAndToSquare.m_RowOrigin + captureDirections[i, 0];
                int potentialCol = i_FromAndToSquare.m_ColOrigin + captureDirections[i, 1];
                i_FromAndToSquare.m_RowDestination = potentialRow;
                i_FromAndToSquare.m_ColDestination = potentialCol;

                string IllegalMoveMessage = null;
                if (checkIfLegal(i_FromAndToSquare, i_Board, out IllegalMoveMessage) == true)
                {
                    i_CaptureOptionsArray.Add(i_FromAndToSquare);
                    returnFlag = true;
                }
            }

            return returnFlag;
        }

        internal bool ReEnterAnotherMove(OriginAndDestinationSquare i_PlayerMove, List<OriginAndDestinationSquare> i_CaptureOptionsArray, Board i_Board)
        {
            bool isValidMove = false;

            for (int i = 0; i < i_CaptureOptionsArray.Count; i++)
            {
                if (i_CaptureOptionsArray[i].Equals(i_PlayerMove))
                {
                    MovePiece(i_PlayerMove, i_Board);
                    isValidMove = true;
                }
            }

            return isValidMove;
        }
    }
}