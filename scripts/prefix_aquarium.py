import json
from pathlib import Path

p = Path("c:\\Users\\Nate\\RiderProjects\\Aquarium\\Aquarium\\localization\\eng\\powers.json")
if not p.exists():
    print(f"File not found: {p}")
    raise SystemExit(1)

text = p.read_text(encoding="utf-8")

data = json.loads(text)

out = {}
for k, v in data.items():
    if '.' in k:
        base, suffix = k.split('.', 1)
        low = suffix.lower()
        if low in ("description", "smartdescription", "title"):
            if not base.startswith("AQUARIUM-"):
                new_key = f"AQUARIUM-{base}.{suffix}"
            else:
                new_key = k
        else:
            new_key = k
    else:
        new_key = k
    out[new_key] = v

# Write back with pretty formatting
p.write_text(json.dumps(out, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
print(f"Updated {p} — {len(data)} entries processed, {sum(1 for k in out if k.startswith('AQUARIUM-'))} AQUARIUM- keys now present.")
