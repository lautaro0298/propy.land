import getStyleComputedProperty from './getStyleComputedProperty';
import getBordersSize from './getBordersSize';
import getWindowSizes from './getWindowSizes';
import getScroll from './getScroll';
import getClientRect from './getClientRect';
import isIE from './isIE';

/**
 * Get bounding client rect of given element
 * @method
 * @memberof Popper.Utils
 * @param {HTMLElement} element
 * @return {Object} client rect
 */
export default function getBoundingClientRect(element: HTMLElement): ClientRect {
  let rect = isIE(10)
    ? {
        ...element.getBoundingClientRect(),
        top: (element.getBoundingClientRect().top as number) + getScroll(element, 'top'),
        left: (element.getBoundingClientRect().left as number) + getScroll(element, 'left'),
        bottom: (element.getBoundingClientRect().bottom as number) + getScroll(element, 'top'),
        right: (element.getBoundingClientRect().right as number) + getScroll(element, 'left'),
      }
    : element.getBoundingClientRect();

  const result = {
    left: rect.left,
    top: rect.top,
    width: rect.right - rect.left,
    height: rect.bottom - rect.top,
  };

  const {width: windowWidth, height: windowHeight} = getWindowSizes(element.ownerDocument) || {};

  const width = result.width || element.clientWidth || windowWidth;
  const height = result.height || element.clientHeight || windowHeight;

  let horizScrollbar = element.offsetWidth - width;
  let vertScrollbar = element.offsetHeight - height;

  if (horizScrollbar || vertScrollbar) {
    const styles = getStyleComputedProperty(element);
    horizScrollbar -= getBordersSize(styles, 'x');
    vertScrollbar -= getBordersSize(styles, 'y');

    Object.assign(result, {
      width: result.width - horizScrollbar,
      height: result.height - vertScrollbar,
    });
  }

  return getClientRect(result);
}

// Add missing return statements
function getStyleComputedProperty(element: HTMLElement): CSSStyleDeclaration {
  return window.getComputedStyle(element, null);
}

function isIE(version: number): boolean {
  // ...
  return false; // Add your implementation here
}

function getScroll(element: HTMLElement, axis: 'x' | 'y'): number {
  // ...
  return 0; // Add your implementation here
}

function getClientRect(rect: ClientRect): ClientRect {
  // ...
  return rect; // Add your implementation here
}
