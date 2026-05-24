import re
from pathlib import Path
p=Path(r'c:\Users\Nate\RiderProjects\Aquarium\AquariumCode\Powers')
prop_block='''
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
for f in sorted(p.glob('*.cs')):
    s=f.read_text(encoding='utf-8')
    s_new = re.sub(r':\s*PowerModel', ': CustomPowerModel', s)
    if 'CustomPackedIconPath' in s_new:
        # ensure inheritance updated
        if s_new!=s:
            f.write_text(s_new, encoding='utf-8')
            print(f'Fixed inheritance in {f.name}')
        else:
            print(f'Already has properties {f.name}')
        continue
    # find first class occurrence
    m=re.search(r'class\s+\w+', s_new)
    if not m:
        print('No class in', f.name)
        continue
    brace_pos = s_new.find('{', m.end())
    if brace_pos==-1:
        print('No brace', f.name)
        continue
    insert_pos = brace_pos+1
    s2 = s_new[:insert_pos] + '\n' + prop_block + '\n' + s_new[insert_pos:]
    f.write_text(s2, encoding='utf-8')
    print('Inserted props', f.name)
