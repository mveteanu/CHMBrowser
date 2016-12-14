var treeSelected = null; //last treeNode clicked

//pre-load tree nodes images
var imgPlus = new Image();
imgPlus.src=imgtreenodeplus;
var imgMinus = new Image();
imgMinus.src=imgtreenodeminus;
var imgDot = new Image();
imgPlus.src=imgtreenodedot;


function findNode(el)
{
// Takes element and determines if it is a treeNode.
// If not, seeks a treeNode in its parents.
	while (el != null)
	{
		if (el.className == "treeNode")
		{
			break;
		}
		else
		{
			el = el.parentNode;
		}
	}
	return el;
}


function clickAnchor(el)
{
	expandNode(el.parentNode);
	selectNode(el.parentNode);
	el.blur();
}

function clickAnchor2(el)
{
	expandNode(el.parentNode);
	el.blur();
}


function selectNode(el)
{
// Un-selects currently selected node, if any, and selects the specified node
//
	if (treeSelected != null)
	{
		setSubNodeClass(treeSelected, 'A', 'treeUnselected');
	}
	setSubNodeClass(el, 'A', 'treeSelected');
	treeSelected = el;
}


function setSubNodeClass(el, nodeName, className)
{
// Sets the specified class name on el's first child that is a nodeName element
//
	var child;
	for (var i=0; i < el.childNodes.length; i++)
	{
		child = el.childNodes[i];
		if (child.nodeName == nodeName)
		{
			child.className = className;
			break;
		}
	}
}


function expandCollapse(el)
{
//	If source treeNode has child nodes, expand or collapse view of treeNode
//
	if (el == null)
		return;	//Do nothing if it isn't a treeNode
		
	var child;
	var imgEl;
	for(var i=0; i < el.childNodes.length; i++)
	{
		child = el.childNodes[i];
		if (child.src)
		{
			imgEl = child;
		}
		else if (child.className == "treeSubnodesHidden")
		{
			child.className = "treeSubnodes";
			imgEl.src = imgtreenodeminus;
			break;
		}
		else if (child.className == "treeSubnodes")
		{
			child.className = "treeSubnodesHidden";
			imgEl.src = imgtreenodeplus;
			break;
		}
	}
}


function expandNode(el)
{
//	If source treeNode has child nodes, expand it
//
	var child;
	var imgEl;
	for(var i=0; i < el.childNodes.length; i++)
	{
		child = el.childNodes[i];
		if (child.src)
		{
			imgEl = child;
		}
		if (child.className == "treeSubnodesHidden")
		{
			child.className = "treeSubnodes";
			imgEl.src = imgtreenodeminus;
			break;
		}
	}
}


function syncTree(href)
{
// Selects and scrolls into view the node that references the specified URL
//
	var loc = new String();
	loc = href;
	if (loc.substring(0, 7) == 'file://')
	{
		loc = 'file:///' + loc.substring(7, loc.length);
		loc = loc.replace(/\\/g, '/');
	}
	
	var base = loc.substr(0, loc.lastIndexOf('/') + 1);
	
	var tocEl = findHref(document.getElementById('treeRoot'), loc, base);
	if (tocEl != null)
	{
		selectAndShowNode(tocEl);
	}
}

function findHref(node, href, base)
{
// find the <a> element with the specified href value
//
	var el;
	var anchors = node.getElementsByTagName('A');
	for (var i = 0; i < anchors.length; i++)
	{
		el = anchors[i];
		var aref = new String();
		aref = el.getAttribute('href');
		
		if ((aref.substring(0, 7) != 'http://') 
			&& (aref.substring(0, 8) != 'https://')
			&& (aref.substring(0, 7) != 'file://'))
		{
			aref = base + aref;
		}
		
		if (aref == href)
		{
			return el;
		}
	}
	return null;
}

function selectAndShowNode(node)
{
// Selects and scrolls into view the specified node
//
	var el = findNode(node);
	if (el != null) 
	{
		selectNode(el);
		do 
		{
			expandNode(el);
			el = findNode(el.parentNode);
		} while ((el != null))  
		
		//vertical scroll element into view
		var windowTop;
		var windowBottom;
		var treeDiv = document.getElementById('tree');
		
		var ua = window.navigator.userAgent.toLowerCase();
		if ((i = ua.indexOf('msie')) != -1)
		{
			windowTop = node.offsetTop - treeDiv.scrollTop;
			windowBottom = treeDiv.clientHeight - windowTop - node.offsetHeight;
		}
		else if (ua.indexOf('gecko') != -1)
		{
			windowTop = node.offsetTop - treeDiv.offsetTop - treeDiv.scrollTop;
			windowBottom = treeDiv.clientHeight - windowTop - node.offsetHeight;
		}
		else 
		{
			return;
		}
		
		if (windowTop < 0)
		{
			treeDiv.scrollTop += windowTop - 18;
			return;
		}
		if (windowBottom < 0)
		{
			treeDiv.scrollTop -= windowBottom - 18;
			return;
		}
	}
}


function resizeTree()
{
	var treeDiv = document.getElementById('tree');
	//treeDiv.setAttribute('style', 'width: ' + document.body.offsetWidth + 'px; height: ' + (document.body.offsetHeight - 27) + 'px;');
	treeDiv.style.width = document.documentElement.offsetWidth;
	treeDiv.style.height = document.documentElement.offsetHeight - 27;
}
