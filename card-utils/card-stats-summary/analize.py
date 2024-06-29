import json
import sys
from collections import defaultdict

def analyze_cards(cards):
    total_count = len(cards)
    rarity_count = defaultdict(int)
    type_count = defaultdict(int)
    mana_cost_count = defaultdict(int)
    
    rarity_stats = defaultdict(lambda: {'cost': [], 'health': [], 'attack': []})
    mana_cost_stats = defaultdict(lambda: {'attack': [], 'health': []})

    for card in cards:
        rarity = card['rarity']
        mana_cost = card['manaCost']
        
        rarity_count[rarity] += 1
        type_count[card['type']] += 1
        mana_cost_count[mana_cost] += 1

        rarity_stats[rarity]['cost'].append(card['cost'])
        rarity_stats[rarity]['health'].append(card['health'])
        rarity_stats[rarity]['attack'].append(card['attack'])

        mana_cost_stats[mana_cost]['attack'].append(card['attack'])
        mana_cost_stats[mana_cost]['health'].append(card['health'])

    with open('stats.md', 'w', encoding='utf-8') as f:
        f.write("# Card Collection Statistics\n\n")
        
        f.write(f"## Total card count: {total_count}\n\n")
        
        f.write("## Card count by rarity\n")
        for rarity, count in rarity_count.items():
            f.write(f"- {rarity}: {count}\n")
        f.write("\n")

        f.write("## Card count by type\n")
        for type_, count in type_count.items():
            f.write(f"- {type_}: {count}\n")
        f.write("\n")

        f.write("## Average stats by rarity\n")
        for rarity, stats in rarity_stats.items():
            f.write(f"### {rarity}\n")
            f.write(f"- Cost average: {sum(stats['cost']) / len(stats['cost']):.2f}\n")
            f.write(f"- Health average: {sum(stats['health']) / len(stats['health']):.2f}\n")
            f.write(f"- Attack average: {sum(stats['attack']) / len(stats['attack']):.2f}\n")
        f.write("\n")

        f.write("## Card count by manaCost\n")
        for mana_cost, count in sorted(mana_cost_count.items()):
            f.write(f"- Mana {mana_cost}: {count}\n")
        f.write("\n")

        f.write("## Average stats by manaCost\n")
        for mana_cost, stats in sorted(mana_cost_stats.items()):
            f.write(f"### Mana {mana_cost}\n")
            f.write(f"- Attack average: {sum(stats['attack']) / len(stats['attack']):.2f}\n")
            f.write(f"- Health average: {sum(stats['health']) / len(stats['health']):.2f}\n")
        f.write("\n")

        f.write("## Additional Statistics\n")
        f.write(f"- Number of unique rarities: {len(rarity_count)}\n")
        f.write(f"- Number of unique types: {len(type_count)}\n")
        f.write(f"- Highest mana cost: {max(mana_cost_count.keys())}\n")
        f.write(f"- Lowest mana cost: {min(mana_cost_count.keys())}\n")

    print("Analysis complete. Results saved to stats.md")

if __name__ == "__main__":
    if len(sys.argv) < 2:
        print("Usage: python analyze_cards.py <input_json_file>")
        sys.exit(1)

    input_file = sys.argv[1]

    try:
        with open(input_file, 'r', encoding='utf-8') as f:
            cards = json.load(f)
        
        analyze_cards(cards)

    except FileNotFoundError:
        print(f"Error: The file '{input_file}' was not found.")
    except json.JSONDecodeError:
        print(f"Error: The file '{input_file}' is not a valid JSON file.")
    except Exception as e:
        print(f"An error occurred: {str(e)}")