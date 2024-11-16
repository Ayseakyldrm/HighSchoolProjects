import random
from abc import ABC, abstractmethod


class Player(ABC):
    """Abstract base class representing a player."""
    def __init__(self, name):
        self.name = name
        self.score = 0
    
    @abstractmethod
    def make_move(self):
        pass
    
    @abstractmethod
    def get_move(self):
        pass
    
    def win(self):
        """Increments the player's score."""
        self.score += 1

class ComputerPlayer(Player):
    """Base class for computer-controlled players."""
    def __init__(self, name="Computer"):
        super().__init__(name)
    
    @abstractmethod
    def make_move(self):
        pass

# 2. Subclasses Definition

class HumanPlayer(Player):
    """Represents a human player."""
    def __init__(self, name):
        super().__init__(name)
    
    def make_move(self):
        """Prompts the human player to make a move."""
        print("\nMake your choice:")
        print("1. Rock\n2. Paper\n3. Scissors")
        choice = input("Rock, Paper, Scissors (1, 2, 3): ").strip().lower()
        if choice == '1':
            self._move = 'Rock'
        elif choice == '2':
            self._move = 'Paper'
        elif choice == '3':
            self._move = 'Scissors'
        else:
            print("Invalid choice, defaulting to Rock.")
            self._move = 'Rock'
    
    def get_move(self):
        """Returns the human player's move."""
        return self._move

class RandomComputerPlayer(ComputerPlayer):
    """Represents a computer player making random moves."""
    def __init__(self, name="Computer"):
        super().__init__(name)
    
    def make_move(self):
        """Randomly selects a move."""
        self._move = random.choice(['Rock', 'Paper', 'Scissors'])
    
    def get_move(self):
        """Returns the computer's move."""
        return self._move


def determine_winner(player_move, computer_move):
    """Determines the winner based on the player's and computer's moves."""
    if player_move == computer_move:
        return "It's a tie!"
    
    winning_combos = {
        ('Rock', 'Scissors'): "Rock crushes Scissors. You win!",
        ('Paper', 'Rock'): "Paper wraps Rock. You win!",
        ('Scissors', 'Paper'): "Scissors cuts Paper. You win!"
    }
    
    if (player_move, computer_move) in winning_combos:
        return winning_combos[(player_move, computer_move)]
    else:
        return f"{computer_move} beats {player_move}. You lose."


def main():
    """Main function to run the Rock-Paper-Scissors game."""
    print("Welcome to Rock-Paper-Scissors!")
    player_name = input("Enter your name: ").strip()

    # Initialize players
    player = HumanPlayer(player_name)
    computer = RandomComputerPlayer()

    # Keep track of game history
    game_history = []
    
    while True:
        print("\n--- New Round ---")
        player.make_move()  # Human player makes a move
        computer.make_move()  # Computer makes a random move

        # Get moves and display them
        player_move = player.get_move()
        computer_move = computer.get_move()
        print(f"{player.name}'s move: {player_move}")
        print(f"{computer.name}'s move: {computer_move}")
        
        # Determine and display the result
        result = determine_winner(player_move, computer_move)
        print(result)

        # Update scores
        if "You win" in result:
            player.win()
        elif "You lose" in result:
            computer.win()

        print(f"\n{player.name}'s Score: {player.score}")
        print(f"{computer.name}'s Score: {computer.score}")
        
        # Record game history
        game_history.append(f"{player.name}: {player_move} vs {computer.name}: {computer_move} -> {result}")

        # Ask to continue or quit
        continue_game = input("\nDo you want to play another round? (Yes/No): ").strip().lower()
        if continue_game != 'yes':
            break

    # Display final results
    print("\n--- Game Over ---")
    print(f"Final Scores: {player.name}: {player.score} - {computer.name}: {computer.score}")
    print("\nGame History:")
    for entry in game_history:
        print(entry)
    
    print("Thank you for playing! See you next time.")

if __name__ == "__main__":
    main()
