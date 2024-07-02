import json
import random
import sys

def calculate_mana_cost(health, attack):
    base_cost = random.randint(1, 4)
    
    stat_cost = (health + attack) // 3
    
    total_cost = min(max(base_cost + stat_cost, 1), 8)
    
    return total_cost

if len(sys.argv) < 2:
    print("Usage: python generate.py <input_json_file>")
    sys.exit(1)

input_file = sys.argv[1]
output_file = "cards-with-mana-cost.json"

try:
    with open(input_file, 'r', encoding='utf-8') as f:
        cards = json.load(f)

    for card in cards:
        health = card.get('health', 0)
        attack = card.get('attack', 0)
        
        card['manaCost'] = calculate_mana_cost(health, attack)

    with open(output_file, 'w', encoding='utf-8') as f:
        json.dump(cards, f, indent=2, ensure_ascii=False)

    print(f"Mana costs have been added to the cards and saved to '{output_file}'.")

except FileNotFoundError:
    print(f"Error: The file '{input_file}' was not found.")
except json.JSONDecodeError:
    print(f"Error: The file '{input_file}' is not a valid JSON file.")
except Exception as e:
    print(f"An error occurred: {str(e)}")