from pathlib import Path
import re

p=Path(r'c:\Users\Nate\RiderProjects\Aquarium\AquariumCode\Powers')
want=[
    'using Aquarium.AquariumCode.Extensions;',
    'using BaseLib.Abstracts;',
    'using BaseLib.Extensions;',
    'using Godot;'
]

for f in sorted(p.glob('*.cs')):
    s=f.read_text(encoding='utf-8')
    if any(u in s for u in want):
        print(f'{f.name}: already has one or more')
        continue
    # find last using directive
    m=list(re.finditer(r'^using\s.+?;\s*$', s, flags=re.M))
    insert_at = 0
    if m:
        insert_at = m[-1].end()
        # ensure a newline
        if not s[insert_at-1] == '\n':
            s = s[:insert_at] + '\n' + s[insert_at:]
    # else insert at top
    block='\n'.join(want)+"\n\n"
    s2 = s[:insert_at] + block + s[insert_at:]
    f.write_text(s2, encoding='utf-8')
    print(f'Inserted usings into {f.name}')
