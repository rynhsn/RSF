#!/bin/bash

# Navigasi ke current directory
cd "$(pwd)"

# Mendapatkan daftar file berekstensi .pdb dan menghapusnya
find . -type f -name '*.pdb' -delete

# Mendapatkan daftar file yang berawalan Blazor dan menghapusnya
find . -type f -name 'Blazor*' -delete

# Mendapatkan daftar file yang berawalan Microsoft dan menghapusnya
find . -type f -name 'Microsoft*' -delete

# Mendapatkan daftar file yang berawalan System dan menghapusnya
find . -type f -name 'System*' -delete

# Mendapatkan daftar file yang berawalan R_ kecuali R_BlazorFrontEnd.Controls.resources.dll
find . -type f -name 'R_*' ! -name 'R_BlazorFrontEnd.Controls.resources.dll' -delete

echo "Cleaning File for Deploy to Front Complete!"
