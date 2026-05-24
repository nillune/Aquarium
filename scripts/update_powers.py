import re
from pathlib import Path

p = Path(r"c:\Users\Nate\RiderProjects\Aquarium\AquariumCode\Powers")
if not p.exists():
    print("Powers folder not found")
    raise SystemExit(1)

prop_block = '''
    public override string CustomPackedIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
            
            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();
        }
    }

    public override string CustomBigIconPath
    {
        get
        {
            var path = $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
           
            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();
        }
    }
'''

cs_files = list(p.glob('*.cs'))

for f in cs_files:
    text = f.read_text(encoding='utf-8')
    original = text
    # replace inheritance
    text = text.replace(': PowerModel', ': CustomPowerModel')

    # remove existing properties if present
    text = re.sub(r'\s*public(?:\s+override)?\s+string\s+CustomPackedIconPath\s*\{.*?\}\s*', '\n', text, flags=re.S)
    text = re.sub(r'\s*public(?:\s+override)?\s+string\s+CustomBigIconPath\s*\{.*?\}\s*', '\n', text, flags=re.S)

    # find class declaration and insert prop_block after the first '{' following it
    m = re.search(r'public\s+(?:sealed|abstract|partial\s+)?class\s+(\w+)\s*:\s*CustomPowerModel', text)
    if m:
        # find the first brace after the class declaration
        brace_pos = text.find('{', m.end())
        if brace_pos != -1:
            insert_pos = brace_pos + 1
            # check if prop_block already present anywhere in the class (cheap check)
            class_end = text.find('\n}\n', insert_pos)
            snippet = text[insert_pos:insert_pos+400]
            if 'CustomPackedIconPath' not in snippet:
                text = text[:insert_pos] + '\n' + prop_block + text[insert_pos:]

    if text != original:
        f.write_text(text, encoding='utf-8')
        print(f'Updated {f.name}')
    else:
        print(f'No change {f.name}')
