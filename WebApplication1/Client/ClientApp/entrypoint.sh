#!/bin/sh
if [[ ! -z "$AUTH_URI" ]]; then
    sed -i -e "s|\"auth\":.*\,|\"auth\": \""$AUTH_URI"\",|i" /usr/share/nginx/html/launcher/config.json
fi

if [[ ! -z "$MAIN_MENU" ]]; then
    #simple regex with exclude groups for perl
    #perl -i -C -p0e "s~\"mainMenu\"((\s|\n)*):((\s|\n)*)\[[^\]|\[]*\]~\"mainMenu\": $MAIN_MENU~gms" config.json
    #or this shit:
    cd /usr/share/nginx/html/launcher
    export SEPARATOR=$(awk 'BEGIN{printf "%c", 127}')
    sed -i 's/\r//g' config.json && paste -s -d $SEPARATOR config.json > config.json.back
    sed -i -r -e "s~\"mainMenu\"((\s\|\n\|$SEPARATOR)*):((\s|\n|$SEPARATOR)*)\[((\n|\s|$SEPARATOR|\{|\}|\"|\'|\&|\w|:|\/|-|\.|\,|////)*)\]~\"mainMenu\": $MAIN_MENU~i" config.json.back
    tr $SEPARATOR '\n' < config.json.back > config.json
    sed -i 's/$/\r/' config.json
fi

nginx -g 'daemon off;'
