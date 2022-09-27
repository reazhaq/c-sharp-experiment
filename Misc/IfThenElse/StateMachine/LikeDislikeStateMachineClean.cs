namespace Misc.IfThenElse.StateMachine;

public class LikeDislikeStateMachineClean
{
    interface IState
    {
        (int likes, int dislikes) Like(LikeDislikeObject likeDislikeObject);
        (int likes, int dislikes) Dislike(LikeDislikeObject likeDislikeObject);
    }

    class StartState : IState
    {
        public (int likes, int dislikes) Dislike(LikeDislikeObject likeDislikeObject)
        {
            likeDislikeObject.Dislikes++;
            likeDislikeObject.CurrentState = DislikeState.Instance;
            return (likeDislikeObject.Likes, likeDislikeObject.Dislikes);
        }

        public (int likes, int dislikes) Like(LikeDislikeObject likeDislikeObject)
        {
            likeDislikeObject.Likes++;
            likeDislikeObject.CurrentState = LikeState.Instance;
            return (likeDislikeObject.Likes, likeDislikeObject.Dislikes);
        }

        public static StartState Instance = new StartState();
    }

    class LikeState : IState
    {
        public (int likes, int dislikes) Dislike(LikeDislikeObject likeDislikeObject)
        {
            if (likeDislikeObject.Likes > 0) { likeDislikeObject.Likes--; }
            likeDislikeObject.Dislikes++;
            likeDislikeObject.CurrentState = DislikeState.Instance;
            return (likeDislikeObject.Likes, likeDislikeObject.Dislikes);
        }

        public (int likes, int dislikes) Like(LikeDislikeObject likeDislikeObject)
        {
            if (likeDislikeObject.Likes > 0) { likeDislikeObject.Likes--; }
            likeDislikeObject.CurrentState = StartState.Instance;
            return (likeDislikeObject.Likes, likeDislikeObject.Dislikes);
        }

        public static LikeState Instance = new LikeState();
    }

    class DislikeState : IState
    {
        public (int likes, int dislikes) Dislike(LikeDislikeObject likeDislikeObject)
        {
            if (likeDislikeObject.Dislikes > 0) { likeDislikeObject.Dislikes--; }
            likeDislikeObject.CurrentState = StartState.Instance;
            return (likeDislikeObject.Likes, likeDislikeObject.Dislikes);
        }

        public (int likes, int dislikes) Like(LikeDislikeObject likeDislikeObject)
        {
            if (likeDislikeObject.Dislikes > 0) { likeDislikeObject.Dislikes--; }
            likeDislikeObject.Likes++;
            likeDislikeObject.CurrentState = LikeState.Instance;
            return (likeDislikeObject.Likes, likeDislikeObject.Dislikes);
        }

        public static DislikeState Instance = new DislikeState();
    }

    class LikeDislikeObject
    {
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public IState CurrentState { get; set; } = StartState.Instance;

        public LikeDislikeObject(int likes, int dislikes)
        {
            Likes = likes;
            Dislikes = dislikes;
        }

        public (int likes, int dislikes) Like() => CurrentState.Like(this);
        public (int likes, int dislikes) Dislike() => CurrentState.Dislike(this);
    }

    [Fact]
    public void T01LikeLike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((5, 5), r);
        Assert.Equal(StartState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T02LikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T03LikeDislikeLike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T04LikeLikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((5, 5), r);
        Assert.Equal(StartState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T05DislikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 5), r);
        Assert.Equal(StartState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T06DislikeLikeDislike()
    {
        var sut = new LikeDislikeObject(5, 5);

        var r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((6, 5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((5, 6), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T07LikeLikeLike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(StartState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-3, -5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T08LikeDislikeLike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-4, -4), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-3, -4), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T09LikeLikeDislike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -5), r);
        Assert.Equal(StartState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-4, -4), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T10DislikeDislikeDislike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(StartState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-5, -3), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T11DislikeLikeDislike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -4), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-4, -3), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);
    }

    [Fact]
    public void T12DislikeDislikeLike_starting_Negative()
    {
        var sut = new LikeDislikeObject(-5, -5);

        var r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(DislikeState.Instance, sut.CurrentState);

        r = sut.Dislike();
        Assert.Equal((-5, -4), r);
        Assert.Equal(StartState.Instance, sut.CurrentState);

        r = sut.Like();
        Assert.Equal((-4, -4), r);
        Assert.Equal(LikeState.Instance, sut.CurrentState);
    }
}
