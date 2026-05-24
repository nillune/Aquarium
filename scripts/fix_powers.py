import re
from pathlib import Path

p = Path(r"c:\Users\Nate\RiderProjects\Aquarium\AquariumCode\Powers")
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
    orig = text
    # replace inheritance (robust to whitespace)
    text = re.sub(r':\s*PowerModel', ': CustomPowerModel', text)

    # find class declaration
    m = re.search(r'public\s+(?:sealed|abstract|partial\s+)?class\s+(\w+)\s*:\s*CustomPowerModel', text)
    if not m:
        print(f'No class match for {f.name}')
        continue
    # find first brace after match
    start_brace = text.find('{', m.end())
    if start_brace == -1:
        print(f'No opening brace for {f.name}')
        continue
    # find matching closing brace for class
    i = start_brace
    depth = 0
    end_brace = -1
    while i < len(text):
        if text[i] == '{':
            depth += 1
        elif text[i] == '}':
            depth -= 1
            if depth == 0:
                end_brace = i
                break
        i += 1
    if end_brace == -1:
        print(f'No matching closing brace for {f.name}')
        continue
    before = text[:start_brace+1]
    class_body = text[start_brace+1:end_brace]
    after = text[end_brace:]

    # remove any existing CustomPackedIconPath/CustomBigIconPath blocks
    class_body_clean = re.sub(r'\s*public(?:\s+override)?\s+string\s+CustomPackedIconPath\s*\{.*?\}\s*', '\n', class_body, flags=re.S)
    class_body_clean = re.sub(r'\s*public(?:\s+override)?\s+string\s+CustomBigIconPath\s*\{.*?\}\s*', '\n', class_body_clean, flags=re.S)

    # remove accidental duplicated fragments like '.png".PowerImagePath();' leftover
    class_body_clean = class_body_clean.replace('\n\n.png".PowerImagePath();\n            \n            return ResourceLoader.Exists(path) ? path : "power.png".PowerImagePath();\n        }\n    }\n', '\n')
    class_body_clean = class_body_clean.replace('\n\n.png".BigPowerImagePath();\n           \n            return ResourceLoader.Exists(path) ? path : "power.png".BigPowerImagePath();\n        }\n    }\n', '\n')

    # trim leading/trailing whitespace
    class_body_clean = class_body_clean.strip('\n')

    new_text = before + '\n' + prop_block + '\n' + class_body_clean + '\n' + after
    if new_text != orig:
        f.write_text(new_text, encoding='utf-8')
        print(f'Fixed {f.name}')
    else:
        print(f'No changes for {f.name}')
