import * as WatchJS from 'melanke-watchjs';

export const watch = WatchJS.watch;

export const unwatch = WatchJS.unwatch;

export const callWatchers =  WatchJS.callWatchers;

export function asignWithRewatch(target: any, source: any) {
  const wField = 'watchers';
  let wathcer: any = null;
  if (!! target && !! source) {
    if (source.hasOwnProperty(wField)) {
      for (const obj in source[wField]) {
        if (Object.prototype.toString.call(source[wField][obj]) === '[object Array]' && source[wField][obj][0] instanceof Function) {
            wathcer = source[wField][obj][0];
        }
      }
    }
    unwatch(source);
    Object.assign(target, source);
    watch(target, wathcer);
    source[wField] = {};
  }
}
