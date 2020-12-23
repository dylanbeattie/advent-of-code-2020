class ListNode {
    constructor(prevNode, value) {
        this.prev = prevNode;
        if (this.prev)
            this.prev.next = this;
        this.value = value;
    }

    static BuildLoop(array) {
        var head = new ListNode(null, array[0]);
        var tail = head;
        for (var i = 1; i < array.length; i++)
            tail = new ListNode(tail, array[i]);
        head.prev = tail;
        tail.next = head;
        return head;
    }

    static BuildList(array) {
        let  head = new ListNode(null, array[0]);
        let  tail = head;
        for (let  i = 1; i < array.length; i++) tail = new ListNode(tail, array[i]);
        return head;
    }

    // find(value) {
    //     let node = this;
    //     while(true) {
    //         if (node.value == value) return(node);
    //         if (node.next == null) return(null);
    //         if (node.next == this) return(null);
    //         node = node.next;
    //     }
    // }

    contains(node) {
        if (node == this) return(true);
        if (this.next == null) return(false);
        return(this.next.contains(node));
    }



    dump(node) {
        if (this.next == null) return this.value;
        if (this == node) return ("LOOP");
        if (node == null) node = this;
        return (this.value + ", " + this.next.dump(node));
    }

    toArray(node) {
        if (this.next == null || this.next == node)
            return this.value;
        if (node == null)
            node = this;
        return ([this.value].concat(this.next.toArray(node)));
    }

    traverse(limit) {
        if (limit == 0)
            return;
        console.log(limit, this.value);
        this.next.traverse(--limit);
    }
    // Removes count elements from the list starting AFTER this element, 
    // and return the sub-list that was removed
    splice(count) {
        var startOfListThatWeAreRemoving = this.next;
        var endOfListThatWeAreRemoving = startOfListThatWeAreRemoving;
        for (var i = 1; i < count; i++) endOfListThatWeAreRemoving = endOfListThatWeAreRemoving.next;
        endOfListThatWeAreRemoving.next.prev = this;
        this.next = endOfListThatWeAreRemoving.next;
        startOfListThatWeAreRemoving.prev = null;
        endOfListThatWeAreRemoving.next = null;
        return (startOfListThatWeAreRemoving);
    }
    tail(head) {
        if (this.next == null) return(this);
        if (this.next == head) return(this);
        if (head == null) head = this;
        return(this.next.tail(head));
    }

    insert(list) {
        list.prev = this;
        let temp = this.next;
        this.next = list;
        let tail = list.tail();
        tail.next = temp;
        if (temp != null) temp.prev = tail;
    }
}

module.exports = { ListNode };