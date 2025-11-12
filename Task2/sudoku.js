const validSudoku = [
  [5, 3, 4, 6, 7, 8, 9, 1, 2],
  [6, 7, 2, 1, 9, 5, 3, 4, 8],
  [1, 9, 8, 3, 4, 2, 5, 6, 7],
  [8, 5, 9, 7, 6, 1, 4, 2, 3],
  [4, 2, 6, 8, 5, 3, 7, 9, 1],
  [7, 1, 3, 9, 2, 4, 8, 5, 6],
  [9, 6, 1, 5, 3, 7, 2, 8, 4],
  [2, 8, 7, 4, 1, 9, 6, 3, 5],
  [3, 4, 5, 2, 8, 6, 1, 7, 9]
];

const invalidSudoku = [
  [5, 3, 4, 6, 7, 8, 9, 1, 2],
  [6, 7, 2, 1, 9, 0, 3, 4, 8],
  [1, 0, 0, 3, 4, 2, 5, 6, 0],
  [8, 5, 9, 7, 6, 1, 0, 2, 0],
  [4, 2, 6, 8, 5, 3, 7, 9, 1],
  [7, 1, 3, 9, 2, 4, 8, 5, 6],
  [9, 0, 1, 5, 3, 7, 2, 1, 4],
  [2, 8, 7, 4, 1, 9, 6, 3, 5],
  [3, 0, 0, 4, 8, 1, 1, 7, 9]
];

function validSolution(sudokuBoard) {
  const validRow = [1, 2, 3, 4, 5, 6, 7, 8, 9];

  function arraysAreEqual(arr1, arr2) {
    if (arr1.length !== arr2.length) return false;
    for (let i = 0; i < arr1.length; i++) {
      if (arr1[i] !== arr2[i]) return false;
    }
    return true;
  }

  for (let i = 0; i < 9; i++) {
    let row = sudokuBoard[i];
    let sortedRow = [...row].sort((a, b) => a - b);
    if (!arraysAreEqual(sortedRow, validRow)) {
      return false;
    }
  }

  for (let col = 0; col < 9; col++) {
    let column = [];
    for (let row = 0; row < 9; row++) {
      column.push(sudokuBoard[row][col]);
    }
    let sortedColumn = column.sort((a, b) => a - b);
    if (!arraysAreEqual(sortedColumn, validRow)) {
      return false;
    }
  }

  return true;
}

function createBoardElement(board, title, isValid) {
  const container = document.createElement('div');
  container.className = 'board-container';

  const titleEl = document.createElement('div');
  titleEl.className = 'board-title';
  titleEl.textContent = title;
  container.appendChild(titleEl);

  const boardEl = document.createElement('div');
  boardEl.className = 'sudoku-board';

  for (let r = 0; r < 9; r++) {
    for (let c = 0; c < 9; c++) {
      const cell = document.createElement('div');
      cell.className = 'cell';

      if ((c + 1) % 3 === 0 && c !== 8) {
        cell.classList.add('block-border-right');
      }
      if ((r + 1) % 3 === 0 && r !== 8) {
        cell.classList.add('block-border-bottom');
      }

      cell.textContent = board[r][c] === 0 ? '' : board[r][c];
      boardEl.appendChild(cell);
    }
  }
  container.appendChild(boardEl);

  const resultText = document.createElement('div');
  resultText.className = 'result ' + (isValid ? 'valid' : 'invalid');
  resultText.textContent = isValid ? "Valid Sudoku Solution" : "Invalid Sudoku Solution";
  container.appendChild(resultText);

  return container;
}

const container = document.getElementById('container');

const validIsValid = validSolution(validSudoku);
const invalidIsValid = validSolution(invalidSudoku);

container.appendChild(createBoardElement(validSudoku, "Valid Sudoku", validIsValid));
container.appendChild(createBoardElement(invalidSudoku, "Invalid Sudoku", invalidIsValid));
