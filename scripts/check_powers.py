from pathlib import Path
p=Path(r'c:\Users\Nate\RiderProjects\Aquarium\AquariumCode\Powers')
for f in sorted(p.glob('*.cs')):
    s=f.read_text(encoding='utf-8')
    has_packed='CustomPackedIconPath' in s
    has_big='CustomBigIconPath' in s
    inherits_custom=': CustomPowerModel' in s
    print(f.name + ':', 'inherits_custom' if inherits_custom else 'inherits_PowerModel', '|', 'packed' if has_packed else 'no_packed', '|', 'big' if has_big else 'no_big')
